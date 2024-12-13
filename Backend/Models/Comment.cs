using System;
using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Post ID is required.")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [MaxLength(50, ErrorMessage = "User ID cannot exceed 50 characters.")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; } = string.Empty;

        public Post Post { get; set; }
    }
}
