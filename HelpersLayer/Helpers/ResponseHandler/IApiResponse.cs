using System.Net;

namespace HelpersLayer.Helpers.ResponseHandler
{
    public interface IApiResponse<T>
    {
        T? Data { get; set; }
        Dictionary<string, List<string>>? Errors { get; set; }
        object? Meta { get; set; }
        HttpStatusCode StatusCode { get; set; }
        bool Succeeded { get; }

    }
    public interface IApiResponse : IApiResponse<object>
    {
    }

}