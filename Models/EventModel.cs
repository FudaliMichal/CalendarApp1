using Microsoft.EntityFrameworkCore;

namespace CalendarApp1.Models;

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