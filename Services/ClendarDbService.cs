using CalendarApp1.Data;
using CalendarApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp1.Services;

/// <summary>
/// Class used to manage events in database - querying data.
/// </summary>
public class CalendarDbService
{
    private readonly CalendarDbContext _dbContext;

    public CalendarDbService(CalendarDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Method finds a given event and deletes it from database.
    /// </summary>
    /// <param name="e">EventModel</param>
    public async Task DeleteEventAsync(EventModel e)
    {
        await _dbContext.Events
        .Where(x => x.EventName == e.EventName && x.EventInfo == e.EventInfo && x.Date == e.Date)
        .ExecuteDeleteAsync();
    }

    /// <summary>
    /// Method used to edit events in database.
    /// </summary>
    /// <param name="e">Event passed to method to find in database.</param>
    /// <param name="newTitle">New title for edited event.</param>
    /// <param name="newMessage">New contents of edited event.</param>
    /// <param name="dateTime">New date for edited event.</param>
    public async Task EditEventAsync(EventModel e, string newTitle, string newMessage, DateTime dateTime)
    {
        var result = await _dbContext.Events.AsTracking()
            .FirstOrDefaultAsync(x => x.EventName == e.EventName && x.EventInfo == e.EventInfo && x.Date == e.Date);


        if (result is null)
        {
            Console.WriteLine("Database error - missing events.");
            return;
        }
        
        result.EventInfo = newMessage;
        result.EventName = newTitle;
        result.Date = dateTime;
        
        await _dbContext.SaveChangesAsync();
    }
    
    /// <summary>
    /// Method used to create events in database.
    /// </summary>
    /// <param name="dateTime">Date of the event.</param>
    /// <param name="eventTitle">Title of the event.</param>
    /// <param name="eventText">Contents of the event.</param>
    public async Task EventCreateAsync(DateTime dateTime, string eventTitle, string eventText)
    {
        var date = DateOnly.FromDateTime(dateTime);
        
        var result = await _dbContext.Days
            .AsTracking()
            .Include(x => x.EventsList)
            .FirstOrDefaultAsync(x => x.DateOnly == date);

        var dayAlreadyExists = result is not null;
        
        result ??= new Day
        {
            DateOnly = date,
            EventsList = []
        };
        
        var _event = new Event
        {
            EventName = eventTitle,
            EventInfo = eventText,
            Date = dateTime,
            Day = result
        };
        
        result.EventsList.Add(_event);
        await _dbContext.Events.AddAsync(_event);

        if (!dayAlreadyExists)
        {
            await _dbContext.Days.AddAsync(result);
        }
        
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Method used to query day's data from database to access events of that day.
    /// </summary>
    /// <param name="dateOnly">Date of the day.</param>
    /// <returns>DayModel</returns>
    public async Task<DayModel> DayInfoAsync(DateOnly dateOnly)
    {
        var day = await _dbContext.Days
            .AsNoTracking()
            .Include(x => x.EventsList)
            .FirstOrDefaultAsync(x => x.DateOnly == dateOnly);

        var dayModel = new DayModel(dateOnly);

        if (day is {EventsList: {Count: > 0} eList})
        {
            foreach (var e in eList)
            {
                var eModel = new EventModel(e.Date)
                {
                    EventName = e.EventName,
                    EventInfo = e.EventInfo
                };
                
                dayModel.EventsList.Add(eModel);
            }
        }

        return dayModel;
    }
}
