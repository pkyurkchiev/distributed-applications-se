using MC.Website.Utils;
using MC.Website.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MC.Website.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {
            List<MovieVM> moviesVM = new List<MovieVM>();
            using (MoviesReference.MoviesClient service = new MoviesReference.MoviesClient())
            {
                foreach (var item in service.GetMovies())
                {
                    moviesVM.Add(new MovieVM(item));
                }
            }
            return View(moviesVM);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            MovieVM movieVM = new MovieVM();
            using (MoviesReference.MoviesClient service = new MoviesReference.MoviesClient())
            {
                var movieDto = service.GetMovieByID(id);
                movieVM = new MovieVM(movieDto);
            }

            return View(movieVM);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.Genres = LoadDataUtil.LoadGenreData();

            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieVM movieVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MoviesReference.MoviesClient service = new MoviesReference.MoviesClient())
                    {
                        MoviesReference.MovieDto movieDto = new MoviesReference.MovieDto
                        {
                            Title = movieVM.Title,
                            ReleaseDate = movieVM.ReleaseDate,
                            ReleaseCountry = movieVM.ReleaseCountry,
                            Genre = new MoviesReference.GenreDto
                            {
                                Id = movieVM.GenreId
                            }
                        };
                        service.PostMovie(movieDto);
                    }

                    return RedirectToAction("Index");
                }

                ViewBag.Genres = LoadDataUtil.LoadGenreData();
                return View();
            }
            catch
            {
                ViewBag.Genres = LoadDataUtil.LoadGenreData();
                return View();
            }
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            MovieVM movieVM = new MovieVM();
            using (MoviesReference.MoviesClient service = new MoviesReference.MoviesClient())
            {
                var movieDto = service.GetMovieByID(id);
                movieVM = new MovieVM(movieDto);
            }

            ViewBag.Genres = LoadDataUtil.LoadGenreData();

            return View(movieVM);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieVM movieVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (MoviesReference.MoviesClient service = new MoviesReference.MoviesClient())
                    {
                        MoviesReference.MovieDto movieDto = new MoviesReference.MovieDto
                        {
                            Id = movieVM.Id,
                            Title = movieVM.Title,
                            ReleaseDate = movieVM.ReleaseDate,
                            ReleaseCountry = movieVM.ReleaseCountry,
                            Genre = new MoviesReference.GenreDto
                            {
                                Id = movieVM.GenreId
                            }
                        };
                        service.PostMovie(movieDto);
                    }

                    return RedirectToAction("Index");
                }

                ViewBag.Genres = LoadDataUtil.LoadGenreData();
                return View();
            }
            catch
            {
                ViewBag.Genres = LoadDataUtil.LoadGenreData();
                return View();
            }
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            MovieVM movieVM = new MovieVM();

            using (MoviesReference.MoviesClient service = new MoviesReference.MoviesClient())
            {
                MoviesReference.MovieDto movieDto =
                service.GetMovieByID(id);

                movieVM = new MovieVM(movieDto);
            }
            return View(movieVM);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (MoviesReference.MoviesClient service = new MoviesReference.MoviesClient())
            {
                service.DeleteMovie(id);
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
