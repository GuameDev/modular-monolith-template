using ModularMonolith.Template.Domain.Common;

namespace ModularMonolith.Template.Domain.WeatherForecasts;

public sealed class WeatherForecast : Entity, IAggregateRoot
{
    public DateOnly Date { get; private set; }
    public int TemperatureC { get; private set; }
    public WeatherSummary Summary { get; private set; }

    public WeatherForecast(DateOnly date, int tempC, WeatherSummary summary)
    {
        Date = date;
        TemperatureC = tempC;
        Summary = summary;
    }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
