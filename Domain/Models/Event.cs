namespace Domain.Models
{
    public class Event
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime Date { get; set; }

        public LimitedUser Planner { get; set; }

        public ICollection<Table> Tables { get; set; }

    }
}
