using Microsoft.EntityFrameworkCore;

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

    public void AddEvent(string eventTitle, string eventMessage)
    {
        
        var datetime = new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day);
        var @event = new EventModel(datetime)
        {
            EventName = eventTitle,
            EventInfo = eventMessage,
        };
        
        EventsList.Add(@event);
    }
   
}