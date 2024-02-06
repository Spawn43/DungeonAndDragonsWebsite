using System.ComponentModel.DataAnnotations;

namespace DungeonAndDragonsWebsite.Models
{
    public class User
    {
        [Key]
        public required string UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public  string? PasswordSalt { get; set; }
        public  string? PasswordHash { get; set; }
        public required string FirstName { get; set; }
        public required string Surname { get; set; }
        public required DateTime DateOfBirth { get; set; }


    }
}
