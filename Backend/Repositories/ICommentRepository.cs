using NoteApp.Models;
public interface ICommentRepository
{
    Task<Comment> GetCommentByIdAsync(int id);
    Task AddCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(int id);
}
