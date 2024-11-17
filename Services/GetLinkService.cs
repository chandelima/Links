using Links.Repositories;

namespace Links.Services;

public class GetLinkService
{
    private readonly RedirectItemRepository _repository;

    public GetLinkService(RedirectItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<string?> Get(string route)
    {
        var registry = await _repository.GetByRoute(route);

        return registry?.Redirect is not null
            ? registry.Redirect
            : null; 
    }
}
