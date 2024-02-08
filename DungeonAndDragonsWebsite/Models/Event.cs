using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DungeonAndDragonsWebsite.Models
{
    public class Event
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Location {  get; set; }

        public DateTime Date { get; set; }

        public User Planner { get; set; }

        public ICollection<Table> Tables { get; set; }
    }
}
