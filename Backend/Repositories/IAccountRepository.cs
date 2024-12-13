// Repositories/IAccountRepository.cs
using Microsoft.AspNetCore.Identity;
using NoteApp.Models;
using System.Threading.Tasks;

namespace NoteApp.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}