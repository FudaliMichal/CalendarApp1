namespace CalendarApp1.Data;

/// <summary>
/// Class representing properties of a day in database.
/// </summary>
public class Day
{
    public int DayID { get; set; }
    public DateOnly DateOnly { get; set; }
    public List<Event> EventsList { get; set; }
}