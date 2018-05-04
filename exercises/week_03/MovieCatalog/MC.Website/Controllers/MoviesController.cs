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
                    moviesVM.Add(new MovieVM
                    {
                        Id = item.Id,
                        Title = item.Title,
                        ReleaseDate = item.ReleaseDate,
                        ReleaseCountry = item.ReleaseCountry
                    });
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
                var movie = service.GetMovieByID(id);
                movieVM = new MovieVM
                {
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ReleaseCountry = movie.ReleaseCountry,
                    GenreId = movie.Genre.Id,
                    GenreVM = new GenreVM {
                        Id = movie.Genre.Id,
                        Name = movie.Genre.Name
                    }
                };
            }

            return View(movieVM);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            using (GenresReference.GenresClient service = new GenresReference.GenresClient())
            {
                ViewBag.Genres = new SelectList(service.GetGenres(), "Id", "Name");
            }

            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieVM movieVM)
        {
            try
            {
                using (MoviesReference.MoviesClient service = new MoviesReference.MoviesClient())
                {
                    MoviesReference.MovieDto movieDto = new MoviesReference.MovieDto
                    {
                        Title = movieVM.Title,
                        ReleaseDate = movieVM.ReleaseDate,
                        ReleaseCountry = movieVM.ReleaseCountry,
                        Genre = new MoviesReference.GenreDto {
                            Id = movieVM.GenreId
                        }
                    };
                    service.PostMovie(movieDto);
                }

                using (GenresReference.GenresClient service = new GenresReference.GenresClient())
                {
                    ViewBag.Genres = new SelectList(service.GetGenres(), "Id", "Name");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            MovieVM movieVM = new MovieVM();
            using (MoviesReference.MoviesClient service = new MoviesReference.MoviesClient())
            {
                var movie = service.GetMovieByID(id);
                movieVM = new MovieVM
                {
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ReleaseCountry = movie.ReleaseCountry,
                    GenreId = movie.Genre.Id
                };
            }

            using (GenresReference.GenresClient service = new GenresReference.GenresClient())
            {
                ViewBag.Genres = new SelectList(service.GetGenres(), "Id", "Name");
            }

            return View(movieVM);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieVM movieVM)
        {
            try
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

                using (GenresReference.GenresClient service = new GenresReference.GenresClient())
                {
                    ViewBag.Genres = new SelectList(service.GetGenres(), "Id", "Name");
                }

                return RedirectToAction("Index");
            }
            catch
            {
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

                movieVM = new MovieVM
                {
                    Id = movieDto.Id,
                    Title = movieDto.Title,
                    ReleaseDate = movieDto.ReleaseDate,
                    ReleaseCountry = movieDto.ReleaseCountry,
                    GenreId = movieDto.Genre.Id,
                    GenreVM = new GenreVM
                    {
                        Id = movieDto.Genre.Id,
                        Name = movieDto.Genre.Name
                    }
                };
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
