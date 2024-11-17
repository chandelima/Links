﻿using Pages.Models;
using System.Text.Json;

namespace Links.Services;

public class GetLinkService
{
    private const string DIRETORIO_JSONS = "/app/";
    private const string ARQUIVO_JSON = "pages.json";

    private IEnumerable<RedirectItem> LoadJson()
    {
        var path = Path.Combine(DIRETORIO_JSONS, ARQUIVO_JSON);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"O arquivo de configuração não foi encontrado: {path}");
        }

        var jsonContent = File.ReadAllText(path);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var config = JsonSerializer
            .Deserialize<List<RedirectItem>>(jsonContent, options)?
            .ToList() ?? [];

        return config;
    }

    public string? Get(string route)
    {
        var routes = LoadJson().ToList();
        var registry = routes.FirstOrDefault(x => x.Route == route);

        return registry?.Redirect is not null
            ? registry.Redirect
            : null; 
    }
}
