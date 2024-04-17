using CalendarApp1.Components;
using CalendarApp1.Services;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp1.Models;

public class MonthModel
{
    public required int Year { get; init; }
    public required List<DayModel> DaysList { get; init; }
    public required string MonthName { get; init; }
    public required int StartDay { get; init; }
    

    public static async Task<MonthModel> MonthInfoAsync(DateOnly date, CalendarDbService dbService)
    {
        var monthSize = DateTime.DaysInMonth(date.Year, date.Month);
        var daysList = new List<DayModel>(monthSize);

        var year = date.Year;
        
        var month = (MonthName)date.Month;
        var monthName = month.ToString();

        var monthBegin = new DateTime(date.Year, date.Month, 1);
        var startDay = (int) monthBegin.DayOfWeek;

        for (var i = 0; i < monthSize; i++)
        {
            var curDate = DateOnly.FromDateTime(monthBegin.AddDays(i));

            daysList.Add(await dbService.DayInfoAsync(curDate));
        }
        
        return new MonthModel()
        {
            DaysList = daysList,
            Year = year,
            MonthName = monthName,
            StartDay = startDay
        };
    }
    
}

public enum MonthName
{
    January = 1,
    February,
    March,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December
}