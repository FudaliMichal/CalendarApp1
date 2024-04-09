using CalendarApp1.Components;

namespace CalendarApp1.Models;

public class MonthModel
{
    // private int _numOfDays { get; }
    public int Year { get; }
    public List<DayModel> DaysList { get; }
    public string MonthName { get; }
    
    public int StartDay { get; }

    public MonthModel(DateOnly date)
    {
        var monthSize = DateTime.DaysInMonth(date.Year, date.Month);
        DaysList = new List<DayModel>(monthSize);

        Year = date.Year;
        
        var month = (MonthName)date.Month;
        MonthName = month.ToString();

        var monthBegin = new DateTime(date.Year, date.Month, 1);
        StartDay = (int) monthBegin.DayOfWeek;

        for (var i = 0; i < monthSize; i++)
        {
            var curDate = DateOnly.FromDateTime(monthBegin.AddDays(i));
            
            DaysList.Add(new DayModel(curDate));
        }
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