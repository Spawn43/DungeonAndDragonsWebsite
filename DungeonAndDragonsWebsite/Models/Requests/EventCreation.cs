namespace DungeonAndDragonsWebsite.Models.Requests
{
    public class EventCreation
    {
        public string LoginToken { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int NumberOfTables {  get; set; }
        public string EventName { get; set; }

    }
}
