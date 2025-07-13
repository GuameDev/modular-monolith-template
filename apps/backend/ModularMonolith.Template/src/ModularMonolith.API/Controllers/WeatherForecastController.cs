using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Template.Application.WeatherForecasts.UseCases.Queries.Get;
using ModularMonolith.Template.Application.WeatherForecasts.UseCases.Queries.Get.DTOs;
using ModularMonolith.Template.SharedKernel.Mediator.Senders;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ISender _sender;

    public WeatherForecastController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Retrieves the weather forecast for the specified number of days.
    /// </summary>
    /// <remarks>The number of days to forecast is specified in the <see
    /// cref="GetWeatherForecastRequest.Days"/> property. Ensure that the value provided meets any constraints defined
    /// by the API.</remarks>
    /// <param name="request">The request containing the number of days for which the weather forecast is needed.</param>
    /// <returns>An <see cref="IActionResult"/> containing the weather forecast data for the specified number of days. The
    /// response is returned with an HTTP 200 status code if successful.</returns>
    [HttpGet]
    [ProducesDefaultResponseType]
    [ProducesResponseType(typeof(GetWeatherForecastResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetWeatherForecastRequest request)
    {
        var result = await _sender.Send(new GetWeatherForecastQuery(request.Days));
        return Ok(result);
    }
}
