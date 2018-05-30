using MC.ApplicationServices.DTOs;
using MC.ApplicationServices.Implementations;
using MC.WebAPIServices.Messages;
using System.Web.Http;

namespace MC.WebAPIServices.Controllers
{
    public class GenresController : BaseController
    {
        #region Properties
        private readonly GenreManagementService _service = null;
        #endregion

        #region Constructors
        public GenresController()
        {
            _service = new GenreManagementService();
        }
        #endregion

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Json(_service.Get());
        }

        [HttpPost]
        public IHttpActionResult Save(GenreDto genreDto)
        {
            if (!genreDto.Validate())
                return Json(new ResponseMessage { Code = 500, Error = "Data is not valid!" });
            ResponseMessage response = new ResponseMessage();

            if (_service.Save(genreDto))
            {
                response.Code = 200;
                response.Body = "Genre is save.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Genre is not save.";
            }

            return Json(response);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (_service.Delete(id))
            {
                response.Code = 200;
                response.Body = "Genre is save.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Genre is not save.";
            }

            return Json(response);
        }
    }
}