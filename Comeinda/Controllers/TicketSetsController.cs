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
using Comeinda.Data.Extension;

namespace Comeinda.Controllers
{
    public class TicketSetsController : Controller
    {
        private readonly ITicketSetsRepository ticketSetsRepository;
        private readonly ITicketRepository ticketRepository;
        private readonly IEventRepository eventRepository;

        public TicketSetsController(ITicketSetsRepository ticketSetsRepository,
            ITicketRepository ticketRepository, IEventRepository eventRepository)
        {
            this.ticketSetsRepository = ticketSetsRepository;
            this.ticketRepository = ticketRepository;
            this.eventRepository = eventRepository;
        }

        // GET: TicketSets
        public  IActionResult Index()
        {
            return View(ticketSetsRepository.GetWithInclude(x => x.Event));
        }

        // GET: TicketSets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketSetsTable = ticketSetsRepository.GetWithInclude(z => z.Id == id.Value, x => x.Event);
            if (ticketSetsTable == null)
            {
                return NotFound();
            }

            return View(ticketSetsTable);
        }

        // GET: TicketSets/Create
        public async Task<IActionResult> Create()
        {
            ViewData["EventId"] = new SelectList(await eventRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: TicketSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomCapacityType,CostOfStandingTickets,CostOfSeatingTickets,NumberOfStandingPlace,Rows,NumberOfPlacesInRow,EventId")] TicketSetsTable ticketSetsTable)
        {
            if (ModelState.IsValid)
            {
                ticketSetsTable.Id = Guid.NewGuid();
                await ticketSetsRepository.CreateAsync(ticketSetsTable);
                await ticketSetsTable.AddTickets(ticketRepository);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(await eventRepository.GetAllAsync(), "Id", "Name");
            return View(ticketSetsTable);
        }

        // GET: TicketSets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketSetsTable = ticketSetsRepository.GetWithInclude(z => z.Id == id.Value, x => x.Event).FirstOrDefault();
            if (ticketSetsTable == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(await eventRepository.GetAllAsync(), "Id", "Name");
            return View(ticketSetsTable);
        }

        // POST: TicketSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RoomCapacityType,CostOfStandingTickets,CostOfSeatingTickets,NumberOfStandingPlace,Rows,NumberOfSeatsInRow,EventId")] TicketSetsTable ticketSetsTable)
        {
            if (id != ticketSetsTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await ticketSetsRepository.UpdateAsync(ticketSetsTable);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketSetsTableExists(ticketSetsTable.Id))
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
            ViewData["EventId"] = new SelectList(await eventRepository.GetAllAsync(), "Id", "Name");
            return View(ticketSetsTable);
        }

        // GET: TicketSets/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketSetsTable = ticketSetsRepository.GetWithInclude(z => z.Id == id.Value, x => x.Event).FirstOrDefault();
            if (ticketSetsTable == null)
            {
                return NotFound();
            }

            return View(ticketSetsTable);
        }

        // POST: TicketSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await ticketSetsRepository.RemoveByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketSetsTableExists(Guid id)
        {
            return ticketSetsRepository.FindByIdAsync(id).Result == null ? false : true;
        }
    }
}
