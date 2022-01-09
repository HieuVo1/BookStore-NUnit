using System.Net;

namespace Book_store.DTOs.Commons
{
    public class APISuccessResponse<T>:APIResponse<T>
    {
        public APISuccessResponse(T data, HttpStatusCode statusCode = HttpStatusCode.OK, dynamic metadata = null)
        {
            IsSuccess = true;
            Data = data;
            StatusCode = (int)statusCode;
            Metadata = metadata;
        }
    }
}
