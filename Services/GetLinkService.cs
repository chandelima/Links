using Microsoft.Extensions.Options;
using Pages.Models;

namespace Links.Services;

public class GetLinkService
{
    private readonly List<RedirectItem> _links;

    public GetLinkService(IOptions<List<RedirectItem>> links)
    {
        _links = links?.Value ?? [];
    }

    public string? Get(string route)
    {
        var registry = _links.FirstOrDefault(x => x.Route == route);

        return registry?.Redirect is not null
            ? registry.Redirect
            : null; 
    }
}
