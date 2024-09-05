using AutoMapper;
using HelpersLayer.Helpers.Bases;
using HelpersLayer.Helpers.Repositorys.GenaricBase.Interface;
using HelpersLayer.Helpers.ResponseHandler;
using InfrastructureLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesLayer.Features.Accounts.Mapping;
using ServicesLayer.Features.Accounts.Requests;
using ServicesLayer.Features.Accounts.Responses;
using Shared.Service.Features.Account.Validators;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static HelpersLayer.Helpers.ResponseHandler.ApiResponseHandler;

namespace ServicesLayer.Services.AccountsServices
{
    public class AccountServices : AppService, IAccountsServices
    {
        private readonly IConfiguration configuration;
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
            this.configuration = configuration;
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


        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
        {
            await DoValidationAsync<LoginRequestValidator, LoginRequest>(request);

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                // if (await _userManager.IsEmailConfirmedAsync(user))
                // {
                var rightPassword = await _userManager.CheckPasswordAsync(user, request.Password);
                if (rightPassword)
                {
                    var token = await GenerateJwtTokenAsync(user);
                    var response = new LoginResponse(token);
                    return Success(response, "تم الدخول بنجاح");
                }
                // }
            }
            return UnAuthorized<LoginResponse>("يوجد خطا فى البريد الالكتروني او كلمة المرور");
        }

        // =============================  Generate Jwt Token ==================================

        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            //Token claims
            var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName! ),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };
            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384);

            //create token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(20),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
