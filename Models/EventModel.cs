using Microsoft.EntityFrameworkCore;

namespace CalendarApp1.Models;

/// <summary>
/// Class representing an event and its properties - date, title and contents.
/// </summary>
public class EventModel
{
    public DateTime Date { get; }
    public string EventName { get; set; }
    public string EventInfo { get; set; }

    public EventModel(DateTime date)
    {
        Date = date;
        EventName = string.Empty;
        EventInfo = string.Empty;
    }
    
}