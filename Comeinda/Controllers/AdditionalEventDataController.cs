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
using Comeinda.Data.Tables.Enams;
using Comeinda.Models;

namespace Comeinda.Controllers
{
    public class AdditionalEventDataController : Controller
    {
        private readonly IEventRepository eventRepository;

        public AdditionalEventDataController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        // GET: EventTables
        public async Task<IActionResult> Index()
        {
            return View(await eventRepository.GetAllAsync());
        }

        // GET: AdditionalEventData/Edit/5
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
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(EventStatusEnum)).Cast<EventStatusEnum>());
            return View(eventTable);
        }

        // POST: AdditionalEventData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Status,TitleSEO,DescriptionSEO,KeywordsSEO,LinkURL")]
        AdditionalEventDataViewModel eventViewModel)
        {
            if (id != eventViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var eventTable = await eventRepository.FindByIdAsync(id);
                    eventTable.Name = eventViewModel.Name;
                    eventTable.Status = eventViewModel.Status;
                    eventTable.TitleSEO = eventViewModel.TitleSEO;
                    eventTable.DescriptionSEO = eventViewModel.DescriptionSEO;
                    eventTable.KeywordsSEO = eventViewModel.KeywordsSEO;
                    eventTable.LinkURL = eventViewModel.LinkURL;
                    await eventRepository.UpdateAsync(eventTable);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventTableExists(eventViewModel.Id))
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
            return View(eventViewModel);
        }
        private bool EventTableExists(Guid id)
        {
            return eventRepository.FindByIdAsync(id).Result == null ? false : true;
        }
    }
}
