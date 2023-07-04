namespace EventHorizonBackend.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public List<User> Attendees { get; set; }
        public List<UserEvent> UserEvents { get; set; }

    }

}
