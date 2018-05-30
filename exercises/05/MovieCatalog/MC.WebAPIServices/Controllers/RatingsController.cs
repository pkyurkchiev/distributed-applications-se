using MC.ApplicationServices.DTOs;
using MC.ApplicationServices.Implementations;
using MC.WebAPIServices.Messages;
using System.Web.Http;

namespace MC.WebAPIServices.Controllers
{
    public class RatingsController : BaseController
    {
        #region Properties
        private readonly RatingManagementService _service = null;
        #endregion

        #region Constructors
        public RatingsController()
        {
            _service = new RatingManagementService();
        }
        #endregion

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Json(_service.Get());
        }

        [HttpPost]
        public IHttpActionResult Save(RatingDto ratingDto)
        {
            if (!ratingDto.Validate())
                return Json(new ResponseMessage { Code = 500, Error = "Data is not valid!" });
            ResponseMessage response = new ResponseMessage();

            if (_service.Save(ratingDto))
            {
                response.Code = 200;
                response.Body = "Rating is save.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Rating is not save.";
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
                response.Body = "Director is save.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Director is not save.";
            }

            return Json(response);
        }
    }
}