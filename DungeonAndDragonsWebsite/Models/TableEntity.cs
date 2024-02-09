using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DungeonAndDragonsWebsite.Models
{
    public class TableEntity
    {
        [Key]
        public string Id { get; set; }
        public Event Event { get; set; }
        public int PlayersAllowed { get; set; }
        public ICollection<UserEntity> Players { get; set; }       
        public UserEntity DungeonMaster { get; set; }
        
    }
}
