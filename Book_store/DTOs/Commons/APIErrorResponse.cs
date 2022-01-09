using System.Net;
using System.Text.Json;

namespace Book_store.DTOs.Commons
{
    public class APIErrorResponse<T>: APIResponse<T>
    {
        public APIErrorResponse(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
            StatusCode = (int)statusCode;
        }

        public override string ToString()// for global exception handdler
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
