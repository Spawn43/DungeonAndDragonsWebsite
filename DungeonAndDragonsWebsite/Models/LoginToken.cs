using System.ComponentModel.DataAnnotations;

namespace DungeonAndDragonsWebsite.Models
{
    public class LoginToken
    {
        [Key]
        public string Id { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public int TTL { get; set; } = 24;
        public DateTime LoginDateTime { get; set; }
    }
}
