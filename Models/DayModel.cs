namespace CalendarApp1.Models;

public class DayModel
{
    public DateOnly DateOnly { get; }
    
    public List<EventModel> EventsList { get; }

    public DayModel(DateOnly date)
    {
        DateOnly = date;
        EventsList = new List<EventModel>();
    }

   
}