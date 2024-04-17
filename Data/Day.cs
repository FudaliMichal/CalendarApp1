namespace CalendarApp1.Data;

public class Day
{
    public int DayID { get; set; }
    public DateOnly DateOnly { get; set; }
    public List<Event> EventsList { get; set; }
}