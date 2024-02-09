namespace DungeonAndDragonsWebsite.Models.DTOs
{
    public class Table
    {
        public string Id { get; set; }
        public Event Event { get; set; }
        public int PlayersAllowed { get; set; }
        public ICollection<LimtedUser> Players { get; set; }
        public LimtedUser DungeonMaster { get; set; }

    }
}
