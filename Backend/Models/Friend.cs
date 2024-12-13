using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteApp.Models
{
    public class Friend
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [MaxLength(50, ErrorMessage = "User ID cannot exceed 50 characters.")]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }

        [Required(ErrorMessage = "Friend ID is required.")]
        [MaxLength(50, ErrorMessage = "Friend ID cannot exceed 50 characters.")]
        public string? FriendId { get; set; }

        [ForeignKey("FriendId")]
        public IdentityUser? FriendUser { get; set; }
    }
}