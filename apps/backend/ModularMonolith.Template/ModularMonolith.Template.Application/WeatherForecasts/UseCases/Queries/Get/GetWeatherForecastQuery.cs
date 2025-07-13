using ModularMonolith.Template.Application.WeatherForecasts.UseCases.Queries.Get.DTOs;
using ModularMonolith.Template.SharedKernel.Mediator.Handlers;
using ModularMonolith.Template.SharedKernel.Pagination.Responses;

namespace ModularMonolith.Template.Application.WeatherForecasts.UseCases.Queries.Get
{
    public sealed record GetWeatherForecastQuery(int Days) : IRequest<PageListResponse<GetWeatherForecastResponse>>;

}
