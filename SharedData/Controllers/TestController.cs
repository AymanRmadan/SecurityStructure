using HelpersLayer.Helpers.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.Features.Requests;
using ServicesLayer.Services;

namespace HelperData.Controllers
{
    public class TestController : AppControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPaginationRequest request)
        {
            var test = await _testService.GetAllPagination(request);
            return ApiResult(test);
        }

    }
}
