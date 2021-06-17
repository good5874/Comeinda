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
    public class EventsController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly ICategoryRepository categoryRepository;

        public EventsController(IEventRepository eventRepository, ICategoryRepository categoryRepository)
        {
            this.eventRepository = eventRepository;
            this.categoryRepository = categoryRepository;
        }

        // GET: EventTables
        public async Task<IActionResult> Index()
        {
            return View(await eventRepository.GetAllAsync());
        }

        // GET: EventTables/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventTable = await eventRepository.FindByIdAsync(id.Value);
            if (eventTable == null)
            {
                return NotFound();
            }

            return View(eventTable);
        }

        // GET: EventTables/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: EventTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,AgeLimit,CategoryId,Genre,RegistrationClosingDate,DoorOpeningDate,EventStartDate,FinishingEventDate,Address,Place")] EventTable eventTable)
        {
            if (ModelState.IsValid)
            {
                eventTable.Id = Guid.NewGuid();
                await eventRepository.CreateAsync(eventTable);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllAsync(), "Id", "Name");
            return View(eventTable);
        }

        // GET: EventTables/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventTable = await eventRepository.FindByIdAsync(id.Value);
            if (eventTable == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllAsync(), "Id", "Name");
            return View(eventTable);
        }

        // POST: EventTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,AgeLimit,CategoryId,Genre,TitleSEO,DescriptionSEO,KeywordsSEO,LinkURL,RegistrationClosingDate,DoorOpeningDate,EventStartDate,FinishingEventDate,Address,Place,UserId")] EventTable eventTable)
        {
            if (id != eventTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await eventRepository.UpdateAsync(eventTable);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventTableExists(eventTable.Id))
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
            ViewData["CategoryId"] = new SelectList(await categoryRepository.GetAllAsync(), "Id", "Name");
            return View(eventTable);
        }

        // GET: EventTables/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventTable = await eventRepository.FindByIdAsync(id.Value);
            if (eventTable == null)
            {
                return NotFound();
            }

            return View(eventTable);
        }

        // POST: EventTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await eventRepository.RemoveByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EventTableExists(Guid id)
        {
            return eventRepository.FindByIdAsync(id).Result == null ? false : true;
        }
    }
}
