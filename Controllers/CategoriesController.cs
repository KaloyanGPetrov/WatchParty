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
    public class CategoriesController : Controller
    {
        private readonly ICategoryServices _categoryService;

        public CategoriesController(ICategoryServices categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [Authorize(Roles = "Admin")]
        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateAsync(categoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        [Authorize(Roles = "Admin")]
        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [Authorize(Roles = "Admin")]
        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateAsync(categoryDto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CategoryExistsAsync(categoryDto.ID))
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
            return View(categoryDto);
        }


        [Authorize(Roles = "Admin")]
        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [Authorize(Roles = "Admin")]
        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategoryExistsAsync(int id)
        {
            return (await _categoryService.GetByIdAsync(id)) != null;
        }
    }
}
