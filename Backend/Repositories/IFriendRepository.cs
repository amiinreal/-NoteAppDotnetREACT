using NoteApp.Models;

namespace NoteApp.Repositories
{
    public interface IFriendRepository
    {
        Task<IEnumerable<Friend>> GetFriendsByUserIdAsync(string userId);
        Task<Friend?> GetFriendshipAsync(string userId, string friendId);
        Task AddFriendAsync(Friend friendship);
        Task DeleteFriendAsync(string userId, string friendId);
    }
}
