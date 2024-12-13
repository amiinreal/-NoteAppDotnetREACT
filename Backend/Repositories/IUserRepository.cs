// IUserRepository.cs
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoteApp.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityUser> GetUserAsync(ClaimsPrincipal principal);
        Task<string> GetUserIdAsync(ClaimsPrincipal principal);
        Task<IdentityUser> FindByIdAsync(string userId);
        Task<IdentityResult> SetUserNameAsync(IdentityUser user, string userName);
        Task<IdentityResult> SetEmailAsync(IdentityUser user, string email);
        Task<IdentityResult> ChangePasswordAsync(IdentityUser user, string currentPassword, string newPassword);
        Task RefreshSignInAsync(IdentityUser user);
    }
}