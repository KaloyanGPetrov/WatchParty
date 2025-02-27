﻿using System;
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
using WatchParty.Services;
using WatchParty.Services.Abstractions;

namespace WatchParty.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorServices _actorService;
        private readonly IMovieServices _movieService;

        public ActorsController(IActorServices actorService, IMovieServices movieServices)
        {
            _actorService = actorService;
            _movieService = movieServices;
        }
        // GET: Actors
        public async Task<IActionResult> Index()
        {
            return View(await _actorService.GetAllAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _actorService.GetByIdAsync(id.Value);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        [Authorize(Roles = "Admin")]
        // GET: Actors/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Movies = await _movieService.GetAllAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Actors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActorCreateEditDTO actorDto)
        {
            if (ModelState.IsValid)
            {
                await _actorService.CreateAsync(actorDto);
                return RedirectToAction(nameof(Index));
            }
            return View(actorDto);
        }

        [Authorize(Roles = "Admin")]
        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _actorService.GetByIdEditAsync(id.Value);
            if (actor == null)
            {
                return NotFound();
            }
            ViewBag.Movies = await _movieService.GetAllAsync();
            ViewBag.SelectedMovies = actor.Movies.Select(movie => movie.ID).ToList();

            return View(new ActorCreateEditDTO()
            {
                ID = actor.ID,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Age = actor.Age,
                ImageUrl = actor.ImageUrl,
                MoviesIds = actor.MoviesIds
            });
        }

        [Authorize(Roles = "Admin")]
        // POST: Actors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActorCreateEditDTO actorDto)
        {
            if (id != actorDto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _actorService.UpdateAsync(actorDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ActorExistsAsync(actorDto.ID))
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
            return View(actorDto);
        }

        [Authorize(Roles = "Admin")]
        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _actorService.GetByIdAsync(id.Value);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }


        [Authorize(Roles = "Admin")]
        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _actorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ActorExistsAsync(int id)
        {
            return (await _actorService.GetByIdAsync(id)) != null;
        }
    }
}
