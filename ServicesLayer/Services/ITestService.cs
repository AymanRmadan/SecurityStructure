using HelpersLayer.Helpers.ResponseHandler;
using ServicesLayer.Features.Requests;
using ServicesLayer.Features.Responses;

namespace ServicesLayer.Services
{
    public interface ITestService
    {
        Task<ApiResponsePaginated<GetAllPaginationResponse>> GetAllPagination(GetAllPaginationRequest request);

    }
}
