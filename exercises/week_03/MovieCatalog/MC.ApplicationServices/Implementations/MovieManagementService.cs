using MC.ApplicationServices.DTOs;
using MC.Data.Entities;
using MC.Repositories.Implementations;
using System.Collections.Generic;

namespace MC.ApplicationServices.Implementations
{
    public class MovieManagementService
    {
        public List<MovieDto> Get()
        {
            List<MovieDto> moviesDto = new List<MovieDto>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.MovieRepository.Get())
                {
                    moviesDto.Add(new MovieDto
                    {
                        Id = item.Id,
                        Title = item.Title,
                        ReleaseDate = item.ReleaseDate,
                        ReleaseCountry = item.ReleaseCountry,
                        Genre = new GenreDto
                        {
                            Id = item.GenreId,
                            Name = item.Genre.Name
                        }
                    });
                }
            }

            return moviesDto;
        }

        public MovieDto GetById(int id)
        {
            MovieDto movieDto = new MovieDto();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Movie movie = unitOfWork.MovieRepository.GetByID(id);
                movieDto = new MovieDto
                {
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ReleaseCountry = movie.ReleaseCountry,
                    Genre = new GenreDto
                    {
                        Id = movie.GenreId,
                        Name = movie.Genre.Name
                    }
                };
            }

            return movieDto;
        }

        public bool Save(MovieDto movieDto)
        {
            Movie movie = new Movie
            {
                Id = movieDto.Id,
                Title = movieDto.Title,
                ReleaseDate = movieDto.ReleaseDate,
                ReleaseCountry = movieDto.ReleaseCountry,
                GenreId = movieDto.Genre.Id
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (movieDto.Id == 0)
                        unitOfWork.MovieRepository.Insert(movie);
                    else
                        unitOfWork.MovieRepository.Update(movie);

                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Movie movie = unitOfWork.MovieRepository.GetByID(id);
                    unitOfWork.MovieRepository.Delete(movie);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
