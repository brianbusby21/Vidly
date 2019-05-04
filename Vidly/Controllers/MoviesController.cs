using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Migrations;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var movieGenre = _context.Genres.ToList();
            var viewModel = new NewMovieFormViewModel
            {
                MovieGenre = movieGenre 
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]//Set this as a POST request only
        public ActionResult Save(Movie movie) //<-- Model Binding
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie); // Adds to memory only. DB context has a change tracking mechanism
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.MovieGenre = movie.MovieGenre;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges(); // Persist the changes to DB

            return RedirectToAction("Index", "Movies"); // Redirect User back to the list of customers
        }

        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.MovieGenre).ToList();

            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.MovieGenre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new NewMovieFormViewModel
            {
                Movie = movie,
                MovieGenre = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }

        //// GET: Movies/Random
        //public ActionResult Random()
        //{
        //    var movie = new Movie() { Name = "Shrek!" };

        //    var customers = new List<Customer>
        //    {
        //        new Customer{ Name = "Customer 1" },
        //        new Customer{ Name = "Customer 2" }
        //    };

        //    var viewModel = new RandomMovieViewModel
        //    {
        //        Movie = movie,
        //        Customers = customers
        //    };

        //    return View(viewModel);
        //    //return Content("Hello World!");
        //    //return HttpNotFound();
        //    //return new EmptyResult();
        //    //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name"});
        //}

        //public ActionResult Edit(int id)
        //{
        //    return Content("id=" + id);
        //}

        //// movies
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }

        //    if (string.IsNullOrWhiteSpace(sortBy))
        //    {
        //        sortBy = "Name";
        //    }

        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}

        //[Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}

    }
}