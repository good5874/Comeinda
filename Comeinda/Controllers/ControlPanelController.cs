using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comeinda.Controllers
{
    public class ControlPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
