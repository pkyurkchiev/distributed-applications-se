using MC.ApplicationServices.DTOs;
using MC.Data.Contexts;
using MC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.ApplicationServices.Implementations
{
    public class MovieManagementService
    {
        private MovieCatalogDbContext ctx = new MovieCatalogDbContext();

        public List<MovieDto> Get()
        {
            List<MovieDto> moviesDto = new List<MovieDto>();

            foreach (var item in ctx.Movies.ToList())
            {
                moviesDto.Add(new MovieDto {
                    Title = item.Title,
                    ReleaseDate = item.ReleaseDate,
                    ReleaseCountry = item.ReleaseCountry,
                    Genre = new GenreDto {
                        Id = item.GenreId,
                        Name = item.Genre.Name
                    }
                });
            }

            return moviesDto;
        }

        public bool Save(MovieDto movieDto)
        {
            Movie movie = new Movie {
                Title = movieDto.Title,
                ReleaseDate = movieDto.ReleaseDate,
                ReleaseCountry = movieDto.ReleaseCountry,
                GenreId = movieDto.Genre.Id
            };

            try
            {
                ctx.Movies.Add(movie);
                ctx.SaveChanges();

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
                Movie movie = ctx.Movies.Find(id);
                ctx.Movies.Remove(movie);
                ctx.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
