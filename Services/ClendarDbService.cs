using CalendarApp1.Data;
using CalendarApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp1.Services;

public class CalendarDbService
{
    private readonly CalendarDbContext _dbContext;

    public CalendarDbService(CalendarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DeleteEventAsync(EventModel e)
    {
        await _dbContext.Events
        .Where(x => x.EventName == e.EventName && x.EventInfo == e.EventInfo && x.Date == e.Date)
        .ExecuteDeleteAsync();
    }

    public async Task EditEventAsync(EventModel e, string newTitile, string newMessage, DateTime dateTime)
    {
        var result = await _dbContext.Events.AsTracking()
            .FirstOrDefaultAsync(x => x.EventName == e.EventName && x.EventInfo == e.EventInfo && x.Date == e.Date);

        //handluj nulla
        
        result.EventInfo = newMessage;
        result.EventName = newTitile;
        result.Date = dateTime;
        
        await _dbContext.SaveChangesAsync();
    }
    
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
