using HelpersLayer.Helpers.ResponseHandler;
using ServicesLayer.Features.Accounts.Requests;
using ServicesLayer.Features.Accounts.Responses;

namespace ServicesLayer.Services.AccountsServices
{
    public interface IAccountsServices
    {
        Task<ApiResponse<AddRegistrationResponse>> Registration(AddRegistrationRequest request);
        Task<ApiResponse<LoginResponse>> Login(LoginRequest request);
        Task<ApiResponse<string>> SendEmail(EmailModelRequest request);
        Task<ApiResponse<string>> ForgetPassword(string Email);
        Task<ApiResponse<string>> ResetPassword(ResetPasswordRequest resetPasswordDto);


    }
}

public partial class EmailBody
{
    public string Email { get; set; }
    public string EmailToken { get; set; }
    //public string ResetPasswordUrl { get; set; }

    //public static string EmailStringBody(string Email, string EmailToken)//, string ResetPasswordUrl)
    //{
    //     var resetPasswordPage = $"http://localhost:4200/#/reset-password?email={Email}&code={EmailToken}";
    //    return $@"<html>
    //                    <head>
    //                    </head>
    //                    <body style=""margin:0; padding:0; font-family:Arial, Helvetica, sans-serif;"">
    //                        <div style=""height:auto; background:linear-gradient(to top, #c9c9ff 50%, #6e6ef6 90%) no-repeat; width:400px; padding:30px"">
    //                            <div>
    //                                <div>
    //                                    <h1>Reset your Password</h1>
    //                                    <hr>
    //                                    <p>You are receiving this e-mail because you requested a password reset for your Let's program account.</p>
    //                                    <p>Please click on the button below to reset your password:</p>
    //                                    <a href={resetPasswordPage} target=""_blank"" style=""background:#0d6efd; padding:10px; border:none; color:white; border-radius:4px; display:block; margin:0 auto; width:50%; text-align:center; text-decoration:none;"">Reset Password</a><br>
    //                                    <p>Madina System, <br><br>AYMAN Ramadan.</p>
    //                                </div>
    //                            </div>
    //                        </div>
    //                    </body>
    //                </html>";
    //}
}
