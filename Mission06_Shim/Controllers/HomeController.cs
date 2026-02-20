using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // [필수] Include 사용을 위해 추가
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

        // READ: 영화 목록 (Category 정보 포함해서 가져오기)
        public IActionResult MovieList()
        {
            var movies = _context.Movies
                .Include(x => x.Category) // [필수] JOIN 역할
                .OrderBy(x => x.Title)
                .ToList();
            return View(movies);
        }

        // CREATE: 입력 화면
        [HttpGet]
        public IActionResult EnterMovie()
        {
            // 드롭다운 메뉴를 위해 카테고리 목록 전달
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
                // 실패 시 다시 카테고리 목록 전달
                ViewBag.Categories = _context.Categories.ToList();
                return View(response);
            }
        }

        // UPDATE: 수정 화면
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var record = _context.Movies.Single(x => x.MovieId == id);
            ViewBag.Categories = _context.Categories.ToList(); // 수정 시에도 드롭다운 필요
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

        // DELETE: 삭제 화면
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