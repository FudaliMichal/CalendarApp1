namespace CalendarApp1.Models;

public class EventModel
{
    public DateTime Date { get; }

    public string EventName { get; }

    public string EventInfo { get; }

    public EventModel(DateTime date)
    {
        Date = date;
        EventName = string.Empty;
        EventInfo = string.Empty;
    }
}