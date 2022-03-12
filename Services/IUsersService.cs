using BlazorServerTestDynamicAccess.Data;
using BlazorServerTestDynamicAccess.Models.DTOs;

namespace BlazorServerTestDynamicAccess.Services
{
    public interface IUsersService : IDisposable
    {
        Task<string> GetSerialNumberAsync(int userId);
        Task<User> FindUserAsync(string username, string password);
        ValueTask<User> FindUserAsync(int userId);
        Task UpdateUserLastActivityDateAsync(int userId);
        Task<UserRegisterDTO> CreateUserAsync(UserRegisterDTO userRegisterDto);
        IAsyncEnumerable<UserRegisterDTO> GetAllUsersAsync();

    }
}
