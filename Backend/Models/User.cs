using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "Profile picture URL cannot exceed 200 characters.")]
        [Url(ErrorMessage = "The profile picture URL must be a valid URL.")]
        public string? ProfilePictureUrl { get; set; }
    }
}
