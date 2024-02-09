namespace Domain.Models
{
    public class CreateEvent
    {
        public string LoginToken { get; set; }

        public string Name { get; set; }

        public string Location {  get; set; }

        public DateTime Date { get; set; }

        public int NumberOfTables { get; set; }
    }
}
