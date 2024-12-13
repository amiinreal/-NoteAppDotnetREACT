using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteApp.Models;

namespace NoteApp.Data
{
    public class NoteAppContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Friend> Friends { get; set; }

        public NoteAppContext(DbContextOptions<NoteAppContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedUsers(builder);
        }
        private void SeedUsers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<IdentityUser>();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            IdentityUser user1 = new IdentityUser
            {
                Id = "user1-id", // Generate unique IDs or use GUIDs
                UserName = "user1@example.com",
                NormalizedUserName = "USER1@EXAMPLE.COM",
                Email = "user1@example.com",
                NormalizedEmail = "USER1@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Password123!")
            };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            IdentityUser user2 = new IdentityUser
            {
                Id = "user2-id",
                UserName = "user2@example.com",
                NormalizedUserName = "USER2@EXAMPLE.COM",
                Email = "user2@example.com",
                NormalizedEmail = "USER2@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Password123!")
            };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            builder.Entity<IdentityUser>().HasData(user1, user2);
        }
    }
}
