using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comeinda.Data;
using Comeinda.Data.Tables;
using Comeinda.Data.Repositories.Abstract;

namespace Comeinda.Controllers
{
    public class CategoryEventsController : Controller
    {
        private ICategoryRepository categoryRepository;

        public CategoryEventsController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        // GET: CategoryEvents
        public async Task<IActionResult> Index()
        {
            return View(await categoryRepository.GetAllAsync());
        }

        // GET: CategoryEvents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEventTable = await categoryRepository.FindByIdAsync(id.Value);
            if (categoryEventTable == null)
            {
                return NotFound();
            }

            return View(categoryEventTable);
        }

        // GET: CategoryEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CategoryEventTable categoryEventTable)
        {
            if (ModelState.IsValid)
            {
                categoryEventTable.Id = Guid.NewGuid();
                await categoryRepository.CreateAsync(categoryEventTable);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryEventTable);
        }

        // GET: CategoryEvents/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEventTable = await categoryRepository.FindByIdAsync(id.Value);
            if (categoryEventTable == null)
            {
                return NotFound();
            }
            return View(categoryEventTable);
        }

        // POST: CategoryEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] CategoryEventTable categoryEventTable)
        {
            if (id != categoryEventTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await categoryRepository.UpdateAsync(categoryEventTable);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryEventTableExists(categoryEventTable.Id))
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
            return View(categoryEventTable);
        }

        // GET: CategoryEvents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryEventTable = await categoryRepository.FindByIdAsync(id.Value);
            if (categoryEventTable == null)
            {
                return NotFound();
            }

            return View(categoryEventTable);
        }

        // POST: CategoryEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await categoryRepository.RemoveByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryEventTableExists(Guid id)
        {
            return categoryRepository.FindByIdAsync(id).Result != null? true : false;
        }
    }
}
