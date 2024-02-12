namespace Domain.Models
{
    public class Table
    {
        public string Id { get; set; }
        public Event Event { get; set; }
        public int PlayersAllowed { get; set; }
        public ICollection<LimitedUser> Players { get; set; }
        public LimitedUser DungeonMaster { get; set; }

    }
}
