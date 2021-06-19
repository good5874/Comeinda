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
using Comeinda.Models.Files;
using Comeinda.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Comeinda.Controllers
{
    public class EventFilesController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly IFileRepository fileRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EventFilesController(IEventRepository eventRepository,
            IFileRepository fileRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.eventRepository = eventRepository;
            this.fileRepository = fileRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View(
                eventRepository.GetWithInclude(x => x.Files)
                    .Select(x => new EventFilesViewModel()
                    {
                        EventId = x.Id,
                        NameEvent = x.Name,
                        Files = x.Files == null ? 0 : x.Files.Count(),
                    }));
        }

        public IActionResult Files(Guid? EventId)
        {
            if (EventId == null)
            {
                return NotFound();
            }

            return View(fileRepository.GetWithInclude(x => x.EventId == EventId.Value));
        }

        public async Task<IActionResult> CreateFile(Guid? EventId)
        {
            if (EventId == null)
            {
                return NotFound();
            }

            var ev = await eventRepository.FindByIdAsync(EventId.Value);

            if (ev == null)
            {
                return NotFound();
            }

            ViewData["EventId"] = ev.Id;
            ViewData["EventName"] = ev.Name;
            return View();
        }

        public async Task<IActionResult> CreatePoster(Guid? EventId)
        {
            if (EventId == null)
            {
                return NotFound();
            }

            var ev = await eventRepository.FindByIdAsync(EventId.Value);

            if (ev == null)
            {
                return NotFound();
            }

            ViewData["EventId"] = ev.Id;
            ViewData["EventName"] = ev.Name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFile([Bind("Name,EventId")] FileTable fileTable,
            IFormFile uploadedFile)
        {
            var ev = await eventRepository.FindByIdAsync(fileTable.EventId);
            if (ModelState.IsValid && uploadedFile != null && ev != null &&
                !string.IsNullOrEmpty(fileTable.Name))
            {
                string path = $"{webHostEnvironment.WebRootPath}\\Events\\{ev.Id}";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                fileTable.Id = Guid.NewGuid();
                var name = $"\\{ fileTable.Name }.{ uploadedFile.FileName.Split(".")[1]}";
                path += name;
                fileTable.Path = path;
                fileTable.SizeBytes = uploadedFile.Length;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                await fileRepository.CreateAsync(fileTable);
                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Files), ev.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePoster([Bind("EventId")] FileTable fileTable,
            IFormFile uploadedFile)
        {
            var ev = await eventRepository.FindByIdAsync(fileTable.EventId);
            if (ModelState.IsValid && uploadedFile != null && ev != null)
            {
                string path = $"{webHostEnvironment.WebRootPath}\\Events\\{ev.Id}";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                fileTable.Id = Guid.NewGuid();
                fileTable.Name = "Афиша";

                var name = $"\\{ fileTable.Name }.{ uploadedFile.FileName.Split(".")[1]}";
                path += name;
                fileTable.Path = path;
                fileTable.SizeBytes = uploadedFile.Length;

                ev.PosterId = fileTable.Id;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                await fileRepository.CreateAsync(fileTable);
                await eventRepository.UpdateAsync(ev);
                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Files), ev.Id);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileTable = await fileRepository.FindByIdAsync(id.Value);
            if (fileTable == null)
            {
                return NotFound();
            }

            return View(fileTable);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fileTable = await fileRepository.FindByIdAsync(id);
            var ev = await eventRepository.FindByIdAsync(fileTable.EventId);

            if (ev.PosterId == fileTable.EventId)
            {
                ev.PosterId = null;
            }

            System.IO.File.Delete(fileTable.Path);

            await fileRepository.RemoveAsync(fileTable);
            await eventRepository.UpdateAsync(ev);
            return RedirectToAction(nameof(Index));
        }
    }
}
