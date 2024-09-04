using AutoMapper;
using HelpersLayer.Helpers.Bases;
using HelpersLayer.Helpers.Repositorys.GenaricBase.Interface;
using HelpersLayer.Helpers.ResponseHandler;
using InfrastructureLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServicesLayer.Features.Accounts.Mapping;
using ServicesLayer.Features.Accounts.Requests;
using ServicesLayer.Features.Accounts.Responses;
using System.ComponentModel.DataAnnotations;
using static HelpersLayer.Helpers.ResponseHandler.ApiResponseHandler;

namespace ServicesLayer.Services.AccountsServices
{
    public class AccountServices : AppService, IAccountsServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountServices(IConfiguration configuration,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<ApplicationUser> userManager
           ) : base(unitOfWork, mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiResponse<AddRegistrationResponse>> Registration(AddRegistrationRequest request)
        {
            await DoValidationAsync<AddRegistrationRequestValidator, AddRegistrationRequest>(request);

            if (request.Password != request.ConfirmPassword)
            {
                throw new ValidationException("Password and Confirm Password do not match.");
            }

            // Create ApplicationUser object
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors.First().Description);
            }

            // await _userManager.AddToRoleAsync(user, Roles.Student.ToString());

            // Generate the email confirmation code
            //var emailConfirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // var confirmationUrl = ConstructConfirmationUrl(user.Id.ToString(), emailConfirmationCode);

            //  var emailModel = ConstructEmailModel(user.Email, confirmationUrl);
            //  SendEmail(emailModel);

            var response = user.MapToAddRegistrationResponse();
            return Success(response, "تم انشاء الحساب بنجاح");

        }

    }
}
