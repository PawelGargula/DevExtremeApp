using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    {
        private readonly MvcMovieContext _context;

        public PersonController(MvcMovieContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName, BirthDate, Email, Localization, Organization")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        [HttpPost]
        public IActionResult CheckEmailAddress(string email)
        {
            if (_context.Person.Any(p => p.Email == email))
            {
                return Json($"Email {email} is already registered.");
            }

            return Json(true);
        }
    }
}
