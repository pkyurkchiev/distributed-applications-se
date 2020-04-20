using MC.ApplicationServices.DTOs;
using MC.Data.Entities;
using MC.Repositories.Implementations;
using System.Collections.Generic;

namespace MC.ApplicationServices.Implementations
{
    public class DirectorManagementService
    {
        public List<DirectorDto> Get()
        {
            List<DirectorDto> directorsDto = new List<DirectorDto>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                foreach (var item in unitOfWork.DirectorReposiotry.Get())
                {
                    directorsDto.Add(new DirectorDto
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        UserName = item.UserName
                    });
                }
            }

            return directorsDto;
        }

        public bool Save(DirectorDto directorDto)
        {
            Director director = new Director
            {
                Id = directorDto.Id,
                FirstName = directorDto.FirstName,
                LastName = directorDto.LastName,
                UserName = directorDto.UserName
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (directorDto.Id == 0)
                        unitOfWork.DirectorReposiotry.Insert(director);
                    else
                        unitOfWork.DirectorReposiotry.Update(director);
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
                    Director director = unitOfWork.DirectorReposiotry.GetByID(id);
                    unitOfWork.DirectorReposiotry.Delete(director);
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
