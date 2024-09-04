using AutoMapper;
using HelpersLayer.Helpers.Bases;
using HelpersLayer.Helpers.Repositorys.GenaricBase.Interface;
using HelpersLayer.Helpers.ResponseHandler;
using ServicesLayer.Features.Mapping;
using ServicesLayer.Features.Requests;
using ServicesLayer.Features.Responses;
using static HelpersLayer.Helpers.ResponseHandler.ApiResponseHandler;

namespace ServicesLayer.Services
{
    public class TestService : AppService, ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TestService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponsePaginated<GetAllPaginationResponse>> GetAllPagination(GetAllPaginationRequest request)
        {
            var query = _unitOfWork.TestRepository.GetAll();//.Paginate(request.PageSize,request.PageNumber);
            var response = await query.MapToGetAllPaginationResponse();
            var totalCount = await UnitOfWork.TestRepository.CountAsync();
            return Success(response, totalCount, request.PageNumber, request.PageSize);

        }

    }
}
