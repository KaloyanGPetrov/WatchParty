using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchParty.Data;
using WatchParty.Data.Enteties;
using WatchParty.DTOs;
using WatchParty.Services.Abstractions;

namespace WatchParty.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieServices _movieService;
        private readonly IActorServices _actorService;
        private readonly ICategoryServices _categoryServices;


        public MoviesController(IMovieServices movieService, IActorServices actorService, ICategoryServices categoryServices)
        {
            _movieService = movieService;
            _actorService = actorService;
            _categoryServices = categoryServices;   
        }


        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _movieService.GetAllAsync());
        }

        [Route("Movies/Category/{name}")]
        public async Task<IActionResult> Category(string name)
        {
            return View(_movieService.GetByCategory(name));
        }

        [Authorize]
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [Authorize(Roles = "Admin")]
        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Actors = await _actorService.GetAllAsync();
            
            ViewBag.Categories = await _categoryServices.GetAllAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieCreateEditDTO movieDto)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateAsync(movieDto);
                return RedirectToAction(nameof(Index));
            }
            return View(movieDto);
        }

        [Authorize(Roles = "Admin")]
        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdEditAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            ViewBag.Actors = await _actorService.GetAllAsync();
            ViewBag.SelectedActors = movie.Actors.Select(actor => actor.ID).ToList(); 
            ViewBag.Categories = await _categoryServices.GetAllAsync();
            ViewBag.SelectedCategories = movie.Categories.Select(cat => cat.ID).ToList(); 

            return View(new MovieCreateEditDTO()
            {
                ID = movie.ID,
                Name = movie.Name,
                Description = movie.Description,
                ImageUrl = movie.ImageUrl,
                VideoPath = movie.VideoPath,
                ActorIds = movie.ActorIds,
                CategoryIds = movie.CategoryIds

            });
        }

        [Authorize(Roles = "Admin")]
        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieCreateEditDTO movieDto)
        {
            if (id != movieDto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _movieService.UpdateAsync(movieDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MovieExistsAsync(movieDto.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieDto);
        }

        [Authorize(Roles = "Admin")]
        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [Authorize(Roles = "Admin")]
        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MovieExistsAsync(int id)
        {
            return (await _movieService.GetByIdAsync(id)) != null;
        }
    }
}
