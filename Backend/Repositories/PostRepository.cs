using NoteApp.Data;
using NoteApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteApp.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly NoteAppContext _context;
        private readonly ILogger<PostRepository> _logger;

        public PostRepository(NoteAppContext context, ILogger<PostRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching post with ID {Id}", id);
                var post = await _context.Posts
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.Id == id);
                
                if (post == null)
                {
                    _logger.LogWarning("Post with ID {Id} not found", id);
                }

                return post!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the post with ID {Id}", id);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all posts");
                return await _context.Posts
                    .OrderByDescending(post => post.CreatedAt)
                    .Include(post => post.Comments)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all posts");
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task AddPostAsync(Post post)
        {
            try
            {
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Added new post with ID {Id}", post.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new post for user {UserId}", post.UserId);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task UpdatePostAsync(Post post)
        {
            try
            {
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated post with ID {Id}", post.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the post with ID {Id}", post.Id);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task DeletePostAsync(int id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);
                if (post != null)
                {
                    _context.Posts.Remove(post);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Deleted post with ID {Id}", id);
                }
                else
                {
                    _logger.LogWarning("Attempt to delete non-existent post with ID {Id}", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the post with ID {Id}", id);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(string userId)
        {
            try
            {
                _logger.LogInformation("Fetching posts for user {UserId}", userId);
                return await _context.Posts
                    .Where(p => p.UserId == userId)
                    .Include(p => p.Comments)
                    .OrderByDescending(p => p.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching posts for user {UserId}", userId);
                throw; // Re-throw to let higher-level handling (e.g., controller) manage it
            }
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.Comments)
                .ToListAsync();
        }

        public async Task CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }
    }
}
