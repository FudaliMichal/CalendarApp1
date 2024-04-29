namespace CalendarApp1.Models;

/// <summary>
/// Class representing weather forecast with properties - date, temperature C/F, and short weather description.
/// </summary>
public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


