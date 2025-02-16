using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhatAndWhenData;
using WhatAndWhenData.Entities;

namespace WhatAndWhen.Controllers
{
    public class PriorityController : Controller
    {
        private readonly WhatAndWhenContext _context;

        public PriorityController(WhatAndWhenContext context)
        {
            _context = context;
        }

        // GET: Priority
        public async Task<IActionResult> Index()
        {
            return View(await _context.Priorities.ToListAsync());
        }

        // GET: Priority/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priorityEntity = await _context.Priorities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priorityEntity == null)
            {
                return NotFound();
            }

            return View(priorityEntity);
        }

        // GET: Priority/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Priority/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Level")] PriorityEntity priorityEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priorityEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priorityEntity);
        }

        // GET: Priority/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priorityEntity = await _context.Priorities.FindAsync(id);
            if (priorityEntity == null)
            {
                return NotFound();
            }
            return View(priorityEntity);
        }

        // POST: Priority/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Level")] PriorityEntity priorityEntity)
        {
            if (id != priorityEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priorityEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriorityEntityExists(priorityEntity.Id))
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
            return View(priorityEntity);
        }

        // GET: Priority/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priorityEntity = await _context.Priorities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (priorityEntity == null)
            {
                return NotFound();
            }

            return View(priorityEntity);
        }

        // POST: Priority/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priorityEntity = await _context.Priorities.FindAsync(id);
            _context.Priorities.Remove(priorityEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriorityEntityExists(int id)
        {
            return _context.Priorities.Any(e => e.Id == id);
        }
    }
}