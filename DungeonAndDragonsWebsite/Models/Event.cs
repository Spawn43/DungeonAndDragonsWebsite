using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DungeonAndDragonsWebsite.Models
{
    public class Event
    {
        [Key]
        public string EventID { get; set; }

        public string EventName { get; set; }

        public string EventLocation {  get; set; }

        public DateTime EventDate { get; set; }
        
        public string EventPlannerID { get; set; }
        public User EventPlanner { get; set; }

        public ICollection<Table> Tables { get; set; }
    }
}
