using MC.ApplicationServices.Interfaces;
using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;
using MC.Data.Contexts;
using MC.Repositories.Implementations;
using MC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MC.ApplicationServices.Implementations
{
    public class GenresManagementService : BaseManagementService, IGenresManagementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenresManagementService(ILogger<GenresManagementService> logger, IUnitOfWork unitOfWork) : base(logger) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetGenresResponse> GetGenres()
        {
            GetGenresResponse response = new() { Genres = new() };
            var genres = await _unitOfWork.Genres.GetAllAsync();

            foreach (var genre in genres)
            {
                response.Genres.Add(new()
                {
                    Name = genre.Name,
                });
            }

            return response;
        }

        public async Task<CreateGenreResponse> CreateGenre(CreateGenreRequest request)
        {
            _unitOfWork.Genres.Insert(new()
            {
                Name = request.Genre.Name,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1,
                IsActivated = true,
            });
            await _unitOfWork.SaveChangesAsync();
            return new();
        }

        public async Task<DeleteGenreResponse> DeleteGenre(DeleteGenreRequest request)
        {
            var genre = await _unitOfWork.Genres.GetByIdAsync(request.Id);

            if (genre == null)
            {
                _logger.LogError("Genre with identifier {request.Id} not found", request.Id);
                throw new Exception("");
            }

            _unitOfWork.Genres.Delete(genre);
            await _unitOfWork.SaveChangesAsync();

            return new();
        }
    }
}
