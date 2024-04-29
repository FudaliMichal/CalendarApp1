namespace CalendarApp1.Data;

/// <summary>
/// Class representing properties of a month in database.
/// </summary>
public class Month
{
    public int MonthID { get; set; }
    
    public int Year { get; set; }
    
    public List<Day> DaysList { get; set; }
    
    public string MonthName { get; set; }
    
    public int StartDay { get; set; }
}