using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorServerTestDynamicAccess.Services
{
    public interface ICookieValidatorService
    {
        Task ValidateAsync(CookieValidatePrincipalContext context);
    }
}
