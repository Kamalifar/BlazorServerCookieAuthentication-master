namespace BlazorServerTestDynamicAccess.Services;

public interface IUserInfoService
{
    Task<string?> GetUserIdAsync();

    Task<string?> GetUserClaimValueAsync(string claimType);
}