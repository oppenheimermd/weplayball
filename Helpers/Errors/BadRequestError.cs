using System.Net;

namespace WePlayBall.Helpers.Api.ApiErrors
{
    public class BadRequestError : ApiError
    {
        public BadRequestError() : base(400, HttpStatusCode.InternalServerError.ToString())
        {
        }


        public BadRequestError(string message) : base(400, HttpStatusCode.InternalServerError.ToString(), message)
        {
        }
    }
}
