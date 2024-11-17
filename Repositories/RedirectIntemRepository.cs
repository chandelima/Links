using Links.Data;
using Links.Models;
using Links.Services;
using Microsoft.EntityFrameworkCore;

namespace Links.Repositories;

public class RedirectItemRepository
{
    private readonly LinksDbContext _context;
    private readonly CacheService _cacheService;

    public RedirectItemRepository(LinksDbContext context, 
                                  CacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<RedirectItem?> GetByRoute(string route)
    {
        var getFromDbTask = () => _context.RedirectItems
            .FirstOrDefaultAsync(x => x.Route == route);

        var item = await _cacheService.GetOrCreateAsync(
            $"redirectItem_{route}",
            getFromDbTask,
            TimeSpan.FromDays(7));

        return item;
    }
}
