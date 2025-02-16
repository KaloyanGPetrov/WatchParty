using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WatchParty.Data;
using WatchParty.Data.Enteties;
using WatchParty.DTOs;
using WatchParty.Services.Abstractions;

namespace WatchParty.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorServices _actorService;

        public ActorsController(IActorServices actorService)
        {
            _actorService = actorService;
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

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActorDTO actorDto)
        {
            if (ModelState.IsValid)
            {
                await _actorService.CreateAsync(actorDto);
                return RedirectToAction(nameof(Index));
            }
            return View(actorDto);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Actors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ActorDTO actorDto)
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
