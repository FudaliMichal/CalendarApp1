namespace CalendarApp1.Data;

/// <summary>
/// Class representing properties of an event in database.
/// </summary>
public class Event
{
    public int EventID { get; set; }
    public  required DateTime Date { get; set; }
    public required Day Day { get; set; }
    public required string EventName { get; set; }
    public required string EventInfo { get; set; }
}