using NoteApp.Models;
using Microsoft.EntityFrameworkCore;
using NoteApp.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApp.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly NoteAppContext _context;
        private readonly ILogger<FriendRepository> _logger;

        public FriendRepository(NoteAppContext context, ILogger<FriendRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Friend>> GetFriendsByUserIdAsync(string userId)
        {
            try
            {
                _logger.LogInformation("Fetching friends for user {UserId}", userId);
                return await _context.Friends
                    .Include(f => f.FriendUser)
                    .Where(f => f.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching friends for user {UserId}", userId);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task<Friend?> GetFriendshipAsync(string userId, string friendId)
        {
            try
            {
                _logger.LogInformation("Fetching friendship between {UserId} and {FriendId}", userId, friendId);
                var friendship = await _context.Friends
                    .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);

                if (friendship == null)
                {
                    _logger.LogWarning("Friendship between {UserId} and {FriendId} not found", userId, friendId);
                }

                return friendship;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching friendship between {UserId} and {FriendId}", userId, friendId);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task AddFriendAsync(Friend friendship)
        {
            try
            {
                await _context.Friends.AddAsync(friendship);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Added new friendship between {UserId} and {FriendId}", friendship.UserId, friendship.FriendId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding friendship between {UserId} and {FriendId}", friendship.UserId, friendship.FriendId);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task DeleteFriendAsync(string userId, string friendId)
        {
            try
            {
                var friendship = await _context.Friends
                    .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);

                if (friendship != null)
                {
                    _context.Friends.Remove(friendship);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Deleted friendship between {UserId} and {FriendId}", userId, friendId);
                }
                else
                {
                    _logger.LogWarning("Attempt to delete non-existent friendship between {UserId} and {FriendId}", userId, friendId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting friendship between {UserId} and {FriendId}", userId, friendId);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }
    }
}
