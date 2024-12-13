using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(1000, ErrorMessage = "Content cannot exceed 1000 characters.")]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [MaxLength(50, ErrorMessage = "User ID cannot exceed 50 characters.")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; } = string.Empty;

        public List<Comment> Comments { get; set; } = new();


        public byte[]? ImageData { get; set; } // Image stored as byte array
    }
}
