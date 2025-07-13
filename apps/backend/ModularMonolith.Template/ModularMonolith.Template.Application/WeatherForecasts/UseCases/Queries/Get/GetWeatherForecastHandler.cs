using ModularMonolith.Template.Application.WeatherForecasts.UseCases.Queries.Get.DTOs;
using ModularMonolith.Template.Domain.WeatherForecasts;
using ModularMonolith.Template.SharedKernel.Mediator.Handlers;
using ModularMonolith.Template.SharedKernel.Pagination.Responses;

namespace ModularMonolith.Template.Application.WeatherForecasts.UseCases.Queries.Get;

public sealed class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastQuery, PageListResponse<GetWeatherForecastResponse>>
{
    public async Task<PageListResponse<GetWeatherForecastResponse>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken = default)
    {
        IEnumerable<GetWeatherForecastResponse> weatherForecasts = [.. Enumerable.Range(1, request.Days).Select(i =>
        {
            var forecast = Generate(DateOnly.FromDateTime(DateTime.Now.AddDays(i)));

            return new GetWeatherForecastResponse(
                forecast.Date,
                forecast.TemperatureC,
                forecast.Summary.ToString()
            );
        })];

        await Task.Delay(10, cancellationToken);

        return new PageListResponse<GetWeatherForecastResponse>()
        {
            Page = 1,
            PageSize = request.Days,
            TotalCount = request.Days,
            Elements = weatherForecasts
        };
    }


    private WeatherForecast Generate(DateOnly date)
    {

        var summary = (WeatherSummary)Random.Shared.Next(Enum.GetValues<WeatherSummary>().Length);
        var temperatureC = Random.Shared.Next(-20, 55);

        return new WeatherForecast(date, temperatureC, summary);
    }
}
