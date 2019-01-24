using System.Net;
using Newtonsoft.Json;

//  See:    https://medium.com/@matteocontrini/consistent-error-responses-in-asp-net-core-web-apis-bb70b435d1f8
// ReSharper disable once CheckNamespace
namespace WePlayBall.Helpers.Api.ApiErrors
{
    /// <summary>
    /// Provide a consistent way to manage possible errors for our API.  The class has 3 properties, that are:
    /// <see cref="StatusCode"/> - The Http integer
    /// <see cref="StatusDescription"/> - The string representation of the status code, which in practice
    /// looks like "NotFound" or "InternalServerError".
    /// <see cref="Message"/> - a friendly message that explains what actually happened.
    /// </summary>
    public class ApiError
    {
        private int StatusCode { get; set; }

        private string StatusDescription { get; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public ApiError(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public ApiError(int statusCode, string statusDescription, string message)
            : this(statusCode, statusDescription)
        {
            this.Message = message;
        }
    }
}
