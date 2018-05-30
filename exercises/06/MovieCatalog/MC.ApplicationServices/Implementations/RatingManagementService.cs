using MC.ApplicationServices.DTOs;
using MC.Data.Entities;
using MC.Repositories.Implementations;
using System.Collections.Generic;

namespace MC.ApplicationServices.Implementations
{
    public class RatingManagementService
    {
        public List<RatingDto> Get()
        {
            List<RatingDto> ratingsDto = new List<RatingDto>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.RatingReposiotry.Get())
                {
                    ratingsDto.Add(new RatingDto
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            }

            return ratingsDto;
        }

        public bool Save(RatingDto ratingDto)
        {
            Rating rating = new Rating
            {
                Id = ratingDto.Id,
                Name = ratingDto.Name
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (ratingDto.Id == 0)
                        unitOfWork.RatingReposiotry.Insert(rating);
                    else
                        unitOfWork.RatingReposiotry.Update(rating);
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
            if (id == 0) return false;

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Rating rating = unitOfWork.RatingReposiotry.GetByID(id);
                    unitOfWork.RatingReposiotry.Delete(rating);
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
