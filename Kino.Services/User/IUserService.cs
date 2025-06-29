using Kino.Core.Authentication;
using Kino.Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Kino.Services.User
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel registerViewModel);
        Task<UserManagerResponse> LoginUserAsync(LoginViewModel loginViewModel);
        Task<List<UpdateUserViewModel>> GetAllUsers();
        Task<IdentityResult> UpdateUser(UpdateUserViewModel user);
        Task<IdentityResult> DeleteUser(string email);
        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
        Task<UpdateUserViewModel> GetUser(string id);
        Task<UserManagerResponse> AddUser(UpdateUserViewModel user);
    }
}
