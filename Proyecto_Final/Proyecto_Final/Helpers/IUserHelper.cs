using Microsoft.AspNetCore.Identity;
using Proyecto_Final.DAL.Entities;
using Proyecto_Final.Models;

namespace Proyecto_Final.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<User> AddUserAsync(AddUserViewModel addUserViewModel);

        Task AddRoleAsync(string roleName); 

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel loginViewModel);

        Task LogoutAsync();

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        //Task<IdentityResult> UpdateUserAsync(User user);

        //Task<User> GetUserAsync(Guid userId); //Sobrecargado
    }
}
