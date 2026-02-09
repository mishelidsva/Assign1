using Assignment1_EventSignup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Assignment1_EventSignup.Controllers
{
    public class EventManagerController : Controller
    {
        private static List<Event> _events = new List<Event>
        {
            new Event { Id = 1, Title = "Career Fair", Date = new DateTime(2026, 2, 1), Location = "Gym" },
            new Event { Id = 2, Title = "Tech Talk", Date = new DateTime(2026, 2, 8), Location = "Auditorium" },
            new Event { Id = 3, Title = "Hack Night", Date = new DateTime(2026, 2, 15), Location = "Library" }
        };

        // GET: /EventManager
        public ActionResult Index()
        {
            ViewBag.Title = "Event Manager";
            return View(_events);
        }

        // GET: /EventManager/Manage/1
        [HttpGet]
        public ActionResult Manage(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var ev = _events.FirstOrDefault(e => e.Id == id.Value);
            if (ev == null) return RedirectToAction("Index");

            ViewBag.Message = TempData["Message"];
            return View(ev);
        }

        [HttpPost]
        public ActionResult Manage(int? id, string name, string email)
        {
            if (id == null) return RedirectToAction("Index");

            var ev = _events.FirstOrDefault(e => e.Id == id.Value);
            if (ev == null) return RedirectToAction("Index");

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email))
            {
                ev.Attendees.Add(new Attendee { Name = name.Trim(), Email = email.Trim() });
                TempData["Message"] = "Attendee registered!";
            }

            return RedirectToAction("Manage", new { id = id.Value });
        }

    }
}
