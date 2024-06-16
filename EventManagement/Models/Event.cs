namespace EventManagement.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public string? BackgroundColor { get; set; }
        public string? BorderColor { get; set; }
        public string? TextColor { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime? Updated_At { get; set; }
    }
}
