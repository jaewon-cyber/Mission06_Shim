using Microsoft.AspNetCore.Mvc;
using Mission06_Shim.Models;
using System.Diagnostics;
using System.Linq; // Required for OrderBy

namespace Mission06_Shim.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieContext _context;

        // Constructor: Injecting the database context
        public HomeController(MovieContext context)
        {
            _context = context;
        }

        // GET: Home Page
        public IActionResult Index()
        {
            return View();
        }

        // GET: Get To Know Joel Page
        public IActionResult GetToKnow()
        {
            return View();
        }

        // GET: Enter Movie Page (Create)
        [HttpGet]
        public IActionResult EnterMovie()
        {
            // Pass a new Movie object to the view to initialize the form
            return View("EnterMovie", new Movie());
        }

        // POST: Enter Movie Page (Create)
        [HttpPost]
        public IActionResult EnterMovie(Movie response)
        {
            if (ModelState.IsValid)
            {
                // Add the new record to the database and save changes
                _context.Movies.Add(response);
                _context.SaveChanges();

                // Show confirmation view
                return View("Confirmation", response);
            }
            else
            {
                // If validation fails, return the form with error messages
                return View(response);
            }
        }

        // GET: Movie List Page (Read)
        [HttpGet]
        public IActionResult MovieList()
        {
            // Retrieve all movies from the database, sorted by Title
            var movies = _context.Movies.OrderBy(x => x.Title).ToList();

            return View(movies);
        }

        // GET: Edit Movie Page (Update)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Find the record that matches the ID passed in the URL
            var record = _context.Movies.Single(x => x.MovieId == id);

            // Reuse the "EnterMovie" view, but pass the existing data
            return View("EnterMovie", record);
        }

        // POST: Edit Movie Page (Update)
        [HttpPost]
        public IActionResult Edit(Movie updatedInfo)
        {
            if (ModelState.IsValid)
            {
                // Update the existing record in the database
                _context.Update(updatedInfo);
                _context.SaveChanges();

                // Redirect to the Movie List page after saving
                return RedirectToAction("MovieList");
            }
            else
            {
                // If validation fails, return the form with the data
                return View("EnterMovie", updatedInfo);
            }
        }

        // GET: Delete Confirmation Page (Delete)
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Find the record to confirm deletion
            var record = _context.Movies.Single(x => x.MovieId == id);

            return View(record);
        }

        // POST: Delete Action (Delete)
        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            // Remove the record from the database
            _context.Movies.Remove(movie);
            _context.SaveChanges();

            // Redirect to the Movie List page
            return RedirectToAction("MovieList");
        }
    }
}