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

        public MoviesController(IMovieServices movieService)
        {
            _movieService = movieService;
        }


        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _movieService.GetAllAsync());
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
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieDTO movieDto)
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

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [Authorize(Roles = "Admin")]
        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieDTO movieDto)
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
