using HelpersLayer.Helpers.ResponseHandler;
using ServicesLayer.Features.Accounts.Requests;
using ServicesLayer.Features.Accounts.Responses;

namespace ServicesLayer.Services.AccountsServices
{
    public interface IAccountsServices
    {
        Task<ApiResponse<AddRegistrationResponse>> Registration(AddRegistrationRequest request);
        Task<ApiResponse<LoginResponse>> Login(LoginRequest request);


    }
}
