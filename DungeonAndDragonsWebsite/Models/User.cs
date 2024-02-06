using System.ComponentModel.DataAnnotations;

namespace DungeonAndDragonsWebsite.Models
{
    public class User
    {
        [Key]
        public required String UserId { get; set; }
        public required String Username { get; set; }
        public required String Email { get; set; }
        public  String? PasswordSalt { get; set; }
        public  String? PasswordHash { get; set; }
        public required String FirstName { get; set; }
        public required String Surname { get; set; }
        public required DateTime DateOfBirth { get; set; }


    }
}
