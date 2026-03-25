using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Mission06_Shim.Models;
using Mission06_Shim.ViewModels;

namespace Mission06_Shim.Controllers;

public class HomeController : Controller
{
    private readonly MovieContext _movieContext;

    public HomeController(MovieContext movieContext)
    {
        _movieContext = movieContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetToKnow()
    {
        return View();
    }

    public IActionResult EnterMovie()
    {
        return View(BuildMovieFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EnterMovie(MovieFormViewModel model)
    {
        model.Ratings = BuildRatings();

        if (!BuildRatings().Any(r => string.Equals(r.Value, model.Rating, StringComparison.OrdinalIgnoreCase)))
        {
            ModelState.AddModelError(nameof(model.Rating), "Rating must be one of: G, PG, PG-13, R.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Keep categories normalized while still allowing free-form entry on the form.
        var categoryName = model.CategoryName.Trim();
        var category = await _movieContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
        if (category is null)
        {
            category = new Category { Name = categoryName };
            _movieContext.Categories.Add(category);
            await _movieContext.SaveChangesAsync();
        }

        var movie = new Movie
        {
            CategoryId = category.CategoryId,
            Title = model.Title.Trim(),
            Year = model.Year!.Value,
            Director = model.Director.Trim(),
            Rating = model.Rating.Trim(),
            Edited = model.Edited,
            LentTo = string.IsNullOrWhiteSpace(model.LentTo) ? null : model.LentTo.Trim(),
            Notes = string.IsNullOrWhiteSpace(model.Notes) ? null : model.Notes.Trim()
        };

        _movieContext.Movies.Add(movie);
        await _movieContext.SaveChangesAsync();

        return RedirectToAction(nameof(Confirmation), new { title = movie.Title });
    }

    public IActionResult Confirmation(string title)
    {
        ViewData["MovieTitle"] = title;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private MovieFormViewModel BuildMovieFormViewModel() =>
        new()
        {
            Ratings = BuildRatings()
        };

    private static IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> BuildRatings() =>
        new[]
        {
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "G", Text = "G" },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "PG", Text = "PG" },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "PG-13", Text = "PG-13" },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "R", Text = "R" }
        };
}