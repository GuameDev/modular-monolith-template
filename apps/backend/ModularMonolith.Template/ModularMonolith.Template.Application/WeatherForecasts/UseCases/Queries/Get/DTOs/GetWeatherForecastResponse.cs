namespace ModularMonolith.Template.Application.WeatherForecasts.UseCases.Queries.Get.DTOs
{
    public sealed record GetWeatherForecastResponse(DateOnly Date, int TemperatureC, string Summary);
}
