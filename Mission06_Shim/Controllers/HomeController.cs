using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Mission06_Shim.Models;
using System.Linq;

namespace Mission06_Shim.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieContext _context;

        public HomeController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();
        public IActionResult GetToKnow() => View();

        
        public IActionResult MovieList()
        {
            var movies = _context.Movies
                .Include(x => x.Category) 
                .OrderBy(x => x.Title)
                .ToList();
            return View(movies);
        }

        
        [HttpGet]
        public IActionResult EnterMovie()
        {
           
            ViewBag.Categories = _context.Categories.ToList();
            return View("EnterMovie", new Movie());
        }

        [HttpPost]
        public IActionResult EnterMovie(Movie response)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(response);
                _context.SaveChanges();
                return View("Confirmation", response);
            }
            else
            {
               
                ViewBag.Categories = _context.Categories.ToList();
                return View(response);
            }
        }

        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var record = _context.Movies.Single(x => x.MovieId == id);
            ViewBag.Categories = _context.Categories.ToList(); 
            return View("EnterMovie", record);
        }

        [HttpPost]
        public IActionResult Edit(Movie updatedInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Update(updatedInfo);
                _context.SaveChanges();
                return RedirectToAction("MovieList");
            }
            else
            {
                ViewBag.Categories = _context.Categories.ToList();
                return View("EnterMovie", updatedInfo);
            }
        }

       
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var record = _context.Movies.Single(x => x.MovieId == id);
            return View(record);
        }

        [HttpPost]
        public IActionResult Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("MovieList");
        }
    }
}