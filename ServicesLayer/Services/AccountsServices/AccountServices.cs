using AutoMapper;
using HelpersLayer.Helpers.Bases;
using HelpersLayer.Helpers.Repositorys.GenaricBase.Interface;
using HelpersLayer.Helpers.ResponseHandler;
using InfrastructureLayer.Context;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

using ServicesLayer.Features.Accounts.Mapping;
using ServicesLayer.Features.Accounts.Requests;
using ServicesLayer.Features.Accounts.Responses;
using Shared.Service.Features.Account.Validators;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static HelpersLayer.Helpers.ResponseHandler.ApiResponseHandler;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ServicesLayer.Services.AccountsServices
{
    public class AccountServices : AppService, IAccountsServices
    {
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUrlHelper urlHelper;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountServices(IConfiguration configuration, IHttpContextAccessor HttpContextAccessor,
            IUnitOfWork unitOfWork, IUrlHelper urlHelper,
            IMapper mapper,
            UserManager<ApplicationUser> userManager
           ) : base(unitOfWork, mapper)
        {
            this.unitOfWork = unitOfWork;
            this.urlHelper = urlHelper;
            this.mapper = mapper;
            _userManager = userManager;
            this.configuration = configuration;
            httpContextAccessor = HttpContextAccessor;
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
            var emailConfirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationUrl = ConstructConfirmationUrl(user.Id.ToString(), emailConfirmationCode);

            var emailModel = ConstructEmailModel(user.Email, confirmationUrl);
            SendEmail(emailModel);

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

        // ============================= Send Email ==================================

        public async Task<ApiResponse<string>> SendEmail(EmailModelRequest request)
        {
            var message = new MimeMessage();
            var from = configuration["EmailSittings:From"];
            message.From.Add(new MailboxAddress("Eng Ayman", from));
            message.To.Add(new MailboxAddress(request.To, request.To));
            message.Subject = request.Subject;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(request.Content)
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(configuration["EmailSittings:SmtpServer"], 587, SecureSocketOptions.StartTls);
                    client.Authenticate(configuration["EmailSittings:UserName"], configuration["EmailSittings:Password"]);

                    await client.SendAsync(message);
                    return Success<string>("Success");
                }
                catch (Exception ex)
                {
                    return BadRequest<string>("Failed");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }


            //end of sending email
        }

        private string ConstructConfirmationUrl(string userId, string code)
        {
            var requestAccessor = httpContextAccessor.HttpContext.Request;
            return $"{requestAccessor?.Scheme}://{requestAccessor.Host}{urlHelper.Action("ConfirmEmail", "Emails", new { UserId = userId, Code = code })}";
        }

        private EmailModelRequest ConstructEmailModel(string recipientEmail, string confirmationUrl)
        {
            var message = $@"<html>
        <head></head>
        <body style=""margin:0; padding:0; font-family:Arial, Helvetica, sans-serif;"">
            <div style=""height:auto; background:linear-gradient(to top, #c9c9ff 50%, #6e6ef6 90%) no-repeat; width:400px; padding:30px"">
                <div>
                    <div>
                        <h1>Confirm your Email</h1>
                        <hr>
                        <p>You are receiving this e-mail because you requested a Confirm Email for your Let's program account.</p>
                        <p>Please click on the button below to Confirm your Email:</p>
                        <a href={confirmationUrl} target=""_blank"" style=""background:#0d6efd; padding:10px; border:none; color:white; border-radius:4px; display:block; margin:0 auto; width:50%; text-align:center; text-decoration:none;"">Confirm Email</a><br>
                        <p>Madina System, <br><br>AYMAN RAMADAN.</p>
                    </div>
                </div>
            </div>
        </body>
    </html>";

            return new EmailModelRequest(recipientEmail, "Confirm Email!!", message);
        }
    }
}
