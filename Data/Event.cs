namespace CalendarApp1.Data;

public class Event
{
    public int EventID { get; set; }

    public  required DateTime Date { get; set; }

    public required Day Day { get; set; }
    
    public required string EventName { get; set; }

    public required string EventInfo { get; set; }
}