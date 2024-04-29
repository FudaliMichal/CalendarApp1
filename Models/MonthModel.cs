using CalendarApp1.Services;

namespace CalendarApp1.Models;

/// <summary>
/// Class representing a month and its properties - year, list of days, name, name of the first day.
/// </summary>
public class MonthModel
{
    public required int Year { get; init; }
    public required List<DayModel> DaysList { get; init; }
    public required string MonthName { get; init; }
    public required int StartDay { get; init; }
    
    /// <summary>
    /// Method used to populate a month model with days from database.
    /// </summary>
    /// <param name="date">Date of chosen month</param>
    /// <param name="dbService"> Database service</param>
    /// <returns>MonthModel</returns>
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

    /// <summary>
    /// Method used to populate the month with both empty days and base with contents from database.
    /// </summary>
    /// <param name="dayList">List of days that are not empty.</param>
    /// <param name="date">Date of the month you want to populate.</param>
    /// <returns>Populated MonthModel</returns>
    public static MonthModel DaysToMonth(List<DayModel> dayList, DateOnly date)
    {
        var monthSize = DateTime.DaysInMonth(date.Year, date.Month);
        var monthBegin = new DateTime(date.Year, date.Month, 1);

        var emptyDays = Enumerable.Range(1, monthSize)
            .Select(x =>
            {
                var curDate = DateOnly.FromDateTime(monthBegin.AddDays(x));
                return new DayModel(curDate);
            }).Where(x => dayList.All(y => y.DateOnly != x.DateOnly))
            .ToList();

        List<DayModel> allDays = new();
        
        allDays.AddRange(dayList);
        allDays.AddRange(emptyDays);
        
        return new MonthModel()
        {
            DaysList = allDays,
            Year = date.Year,
            MonthName = ((MonthName)date.Month).ToString(),
            StartDay = (int) monthBegin.DayOfWeek
        };
    }
}



/// <summary>
/// Enum used to conveniently choose names of months.
/// </summary>
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