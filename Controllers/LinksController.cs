using Microsoft.AspNetCore.Mvc;
using Pages.Models;
using System.Text.Json;
using System.Web;

namespace Pages.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public WeatherForecastController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("{*path}")]
        public IActionResult Get(string path)
        {
            var id = HttpUtility.UrlDecode(path);
            var routes = LoadJson().ToList();
            var route = routes.FirstOrDefault(x => x.Route == id);

            return route?.Redirect is not null
                ? RedirectPermanent(route.Redirect)
                : NotFound();
        }

        private IEnumerable<RedirectItem> LoadJson()
        {
            var path = Path.Combine(_env.ContentRootPath, "Static", "pages.json");

            if (!System.IO.File.Exists(path))
                throw new FileNotFoundException($"O arquivo de configuração não foi encontrado: {path}");

            var jsonContent = System.IO.File.ReadAllText(path);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var config = JsonSerializer
                .Deserialize<List<RedirectItem>>(jsonContent, options)?
                .ToList() ?? [];

            return config;
        }
    }
}
