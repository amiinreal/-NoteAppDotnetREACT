using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace NoteApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<UserRepository> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IdentityUser> GetUserAsync(ClaimsPrincipal principal)
        {
            try
            {
                _logger.LogInformation("Fetching user details.");
                var user = await _userManager.GetUserAsync(principal);
                if (user == null)
                {
                    _logger.LogWarning("User not found for the given principal.");
                    throw new Exception("User not found.");
                }
                return user!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user details.");
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public Task<string> GetUserIdAsync(ClaimsPrincipal principal)
        {
            try
            {
                var userId = _userManager.GetUserId(principal);
                if (userId == null)
                {
                    _logger.LogWarning("User ID not found for the given principal.");
                    throw new Exception("User ID not found.");
                }
                _logger.LogInformation("Fetched user ID: {UserId}", userId);
                return Task.FromResult(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user ID.");
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task<IdentityUser> FindByIdAsync(string userId)
        {
            try
            {
                _logger.LogInformation("Fetching user with ID {UserId}", userId);
                var user = await _userManager.FindByIdAsync(userId);
                
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                }

                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", userId);
                    throw new Exception($"User with ID {userId} not found.");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user with ID {UserId}", userId);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task<IdentityResult> SetUserNameAsync(IdentityUser user, string userName)
        {
            try
            {
                _logger.LogInformation("Setting username for user {UserId}", user.Id);
                return await _userManager.SetUserNameAsync(user, userName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while setting username for user {UserId}", user.Id);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task<IdentityResult> SetEmailAsync(IdentityUser user, string email)
        {
            try
            {
                _logger.LogInformation("Setting email for user {UserId}", user.Id);
                return await _userManager.SetEmailAsync(user, email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while setting email for user {UserId}", user.Id);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(IdentityUser user, string currentPassword, string newPassword)
        {
            try
            {
                _logger.LogInformation("Changing password for user {UserId}", user.Id);
                return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while changing password for user {UserId}", user.Id);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task RefreshSignInAsync(IdentityUser user)
        {
            try
            {
                _logger.LogInformation("Refreshing sign-in for user {UserId}", user.Id);
                await _signInManager.RefreshSignInAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while refreshing sign-in for user {UserId}", user.Id);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }
    }
}
