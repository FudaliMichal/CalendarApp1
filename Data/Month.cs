namespace CalendarApp1.Data;

public class Month
{
    public int MonthID { get; set; }
    
    public int Year { get; set; }
    
    public List<Day> DaysList { get; set; }
    
    public string MonthName { get; set; }
    
    public int StartDay { get; set; }
}