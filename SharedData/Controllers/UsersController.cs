using HelpersLayer.Helpers.Bases;
using Microsoft.AspNetCore.Mvc;
using ServicesLayer.Features.Accounts.Requests;
using ServicesLayer.Services.AccountsServices;

namespace Shared.API.Controllers
{

    [Route("api/[controller]")]
    public class UsersController : AppControllerBase
    {
        private readonly IAccountsServices _accountsService;

        public UsersController(IAccountsServices accountsService)
        {
            _accountsService = accountsService;
        }
        [Route("~/api/auth/register")]
        [HttpPost]
        public async Task<IActionResult> Registration([FromForm] AddRegistrationRequest request)
        {
            var result = await _accountsService.Registration(request);
            return ApiResult(result);
        }
        // [Route("~/api/auth/login")]
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginRequest request)
        //{

        //    var result = await _accountsService.Login(request);

        //    return ApiResult(result);

        //}

    }
}
