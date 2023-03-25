using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using MqProducer.Services;

namespace DotNetCoreRabbitMqQueue.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ProducerService producerService;
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration conf;

    public WeatherForecastController(ProducerService producerService, ILogger<WeatherForecastController> logger, IConfiguration conf)
    {
        this.producerService = producerService;
        _logger = logger;
        this.conf = conf;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var mail = new Mail();
        mail.Header = "First Mail";
        mail.Text = "mail Text";
        mail.ToAddress = "Nurlan.Goyjali.gmail.com";
        var url = conf["MqConnection"];
        producerService.MqProducer(url,mail);
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPost(Name = "createmail")]
    public IActionResult CreateMail(Mail mail)
    {
        var url = conf["MqConnection"];
        producerService.MqProducer(url,mail);
        return Ok();
    }
}