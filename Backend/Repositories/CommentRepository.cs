using NoteApp.Data;
using NoteApp.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace NoteApp.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly NoteAppContext _context;
        private readonly ILogger<CommentRepository> _logger;

        public CommentRepository(NoteAppContext context, ILogger<CommentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Fetching comment with ID {Id}", id);
                var comment = await _context.Comments.FindAsync(id);
                
                if (comment == null)
                {
                    _logger.LogWarning("Comment with ID {Id} not found", id);
                }

                return comment!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the comment with ID {Id}", id);
                throw; // Re-throwing to let higher layers handle if needed
            }
        }

        public async Task AddCommentAsync(Comment comment)
        {
            try
            {
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Added new comment with ID {Id}", comment.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new comment for post {PostId}", comment.PostId);
                throw; // Re-throwing to let higher layers handle if needed
            }
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            try
            {
                _context.Comments.Update(comment);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated comment with ID {Id}", comment.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the comment with ID {Id}", comment.Id);
                throw; // Re-throwing to let higher layers handle if needed
            }
        }

        public async Task DeleteCommentAsync(int id)
        {
            try
            {
                var comment = await _context.Comments.FindAsync(id);
                if (comment != null)
                {
                    _context.Comments.Remove(comment);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Deleted comment with ID {Id}", id);
                }
                else
                {
                    _logger.LogWarning("Attempt to delete non-existent comment with ID {Id}", id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the comment with ID {Id}", id);
                throw; // Re-throwing to let higher layers handle if needed
            }
        }
    }
}
