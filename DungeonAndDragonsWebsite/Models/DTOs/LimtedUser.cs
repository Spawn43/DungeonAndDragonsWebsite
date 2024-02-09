using System.ComponentModel.DataAnnotations;

namespace DungeonAndDragonsWebsite.Models.DTOs
{
    public class LimtedUser
    {

        [Key]
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

    }
}
