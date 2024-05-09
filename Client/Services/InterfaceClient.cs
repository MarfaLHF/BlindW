using BlindW.Data.Models;
using Client.Models;
using Client.Models.Requests;
using Microsoft.AspNetCore.Identity.Data;
using Refit;
namespace Client.Services
{
    public interface InterfaceClient
    {
        [Post("/Account/registration")]
        Task Register(UserRegistrationRequest request);

        [Post("/login")]
        Task<AuthResponse> Login(UserLoginRequest request);

        [Post("/refresh")]
        Task<AuthResponse> Refresh(RefreshTokenRequest request);

        [Get("/confirmEmail")]
        Task ConfirmEmail(ConfirmEmailRequest request);

        [Post("/resendConfirmationEmail")]
        Task ResendConfirmationEmail(ResendConfirmationEmailRequest request);

        [Post("/forgotPassword")]
        Task ForgotPassword(ForgotPasswordRequest request);

        [Post("/resetPassword")]
        Task ResetPassword(ResetPasswordRequest request);

        [Post("/manage/2fa")]
        Task Manage2FA(Manage2FARequest request);

        [Get("/manage/info")]
        Task<UserInfoResponse> GetUserInfo();

        [Post("/manage/info")]
        Task UpdateUserInfo(UserInfoUpdateRequest request);


        [Get("/api/TestResults")]
        Task<IEnumerable<TestResult>> GetTestResults();

        [Get("/api/TestResults/{id}")]
        Task<TestResult> GetTestResult(string id);

        [Put("/api/TestResults/{id}")]
        Task PutTestResult(string id, TestResult testResult);

        [Post("/api/TestResults")]
        Task<TestResult> PostTestResult(Result testResult);

        [Delete("/api/TestResults/{id}")]
        Task DeleteTestResult(string id);
    }
}
