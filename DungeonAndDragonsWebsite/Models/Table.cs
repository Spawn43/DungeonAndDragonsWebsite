using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DungeonAndDragonsWebsite.Models
{
    public class Table
    {
        [Key]
        public string Id { get; set; }
        public Event Event { get; set; }
        public int PlayersAllowed { get; set; }
        public ICollection<User> Players { get; set; }       
        public User DungeonMaster { get; set; }
        
    }
}
