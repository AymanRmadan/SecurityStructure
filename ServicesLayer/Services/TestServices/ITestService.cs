using HelpersLayer.Helpers.ResponseHandler;
using ServicesLayer.Features.GetAll.Requests;
using ServicesLayer.Features.GetAll.Responses;

namespace ServicesLayer.Services.TestServices
{
    public interface ITestService
    {
        Task<ApiResponsePaginated<GetAllPaginationResponse>> GetAllPagination(GetAllPaginationRequest request);

    }
}
