using Microsoft.EntityFrameworkCore;

namespace CalendarApp1.Models;

/// <summary>
/// Class representing a day. Including its events.
/// </summary>
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