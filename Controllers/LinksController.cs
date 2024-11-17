using Links.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Pages.Controllers
{
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly GetLinkService _getLinkService;

        public LinksController(GetLinkService getLinkService)
        {
            _getLinkService = getLinkService;
        }

        [HttpGet("{*route}")]
        public IActionResult Get(string? route)
        {
            string? url = null;
            
            var decodedRoute = HttpUtility.UrlDecode(route) ?? string.Empty;
            url = _getLinkService.Get(decodedRoute);

            return !string.IsNullOrWhiteSpace(url)
                ? Redirect(url)
                : NotFound(string.Empty);
        }
    }
}
