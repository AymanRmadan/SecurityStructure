using System.Net;

namespace HelpersLayer.Helpers.ResponseHandler
{
    public class ApiResponse<T> : IApiResponse<T>//: ApiResponse, IApiResponse<T>
    {
        public T? Data { get; set; }
        public bool Succeeded => (int)StatusCode >= 200 && (int)StatusCode <= 290;
        public HttpStatusCode StatusCode { get; set; }
        public object? Meta { get; set; }
        public string? Message { get; set; }
        public Dictionary<string, List<string>>? Errors { get; set; }
        public ApiResponse()
        {

        }
        public ApiResponse(T data, string? meta = null, string message = null)
        {
            Data = data;
            Meta = meta;
            Message = message;
        }

    }
    public class ApiResponse : ApiResponse<object>, IApiResponse
    {
    }

}
