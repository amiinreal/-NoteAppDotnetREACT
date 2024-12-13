using NoteApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteApp.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId);
        Task<IEnumerable<Post>> GetPostsAsync();
        Task CreatePostAsync(Post post);
    }
}