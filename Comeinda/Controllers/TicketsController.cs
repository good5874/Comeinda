using Comeinda.Data.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Comeinda.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketRepository ticketRepository;

        public TicketsController(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        // GET: Ticket
        public async Task<IActionResult> Index()
        {
            return View(await ticketRepository.GetAllAsync());
        }
    }
}
