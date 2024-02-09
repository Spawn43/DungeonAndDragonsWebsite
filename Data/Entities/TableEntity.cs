using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class TableEntity
    {
        [Key]
        public string Id { get; set; }
        public EventEntity Event { get; set; }
        public int PlayersAllowed { get; set; }
        public ICollection<UserEntity> Players { get; set; }       
        public UserEntity DungeonMaster { get; set; }
        
    }
}
