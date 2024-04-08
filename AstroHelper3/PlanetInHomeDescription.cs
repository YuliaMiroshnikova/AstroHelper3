using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AstroHelper3;

public class PlanetInHomeDescription
{
    private AppDBContext _context;
    private string _outputFilePath;
    private string _jsonFilePath; 
    
    
    public PlanetInHomeDescription(AppDBContext context, string outputFilePath, string jsonFilePath)
    {
        _context = context;
        _outputFilePath = outputFilePath;
        _jsonFilePath = jsonFilePath;
    }

  

    public void GenerateDescriptionsPlanetHome()
    {
        
        List<HomeDescription> descriptions = LoadDescriptions(_jsonFilePath);

        using (var writer = new StreamWriter(_outputFilePath, false))
        {
            var planets = _context.PlanetsDbs.Include(p => p.HomeNavigation).ToList();

            foreach (var planet in planets)
            {
                if (planet.Planets == "Вертекс" || planet.Planets == "Фортуны")
                {
                    continue; 
                }
                
                string description = GenerateDescriptionForPlanetinHome(planet, descriptions);
                writer.WriteLine(description);
            }
        }
    }

    public static List<HomeDescription> LoadDescriptions(string jsonFilePath)
    {
        var jsonString = File.ReadAllText(jsonFilePath);
        return JsonSerializer.Deserialize<List<HomeDescription>>(jsonString) ?? new List<HomeDescription>();
    }

    public string GenerateDescriptionForPlanetinHome(PlanetsDB planet, List<HomeDescription> descriptions)
    {
        var jsonDescription = descriptions.FirstOrDefault(d => d.Planet == planet.Planets && d.Home == planet.Home)?.Description;

        string planetName = planet.Planets;
        if (planetName == "Восхузел")
        {
            planetName = "Раху";
        }
        else if (planetName == "Низхузел")
        {
            planetName = "Кету";
        }
        
        if (!string.IsNullOrEmpty(jsonDescription))
        {
            return $"\n\n{planet.Planets}, Дом: {planet.Home}, Позиция: {planet.Position}. \n" +
                   $"{jsonDescription}\n";
        }

        return $"Планета: {planet.Planets} в доме {planet.Home} \n " +
               $"не имеет специфического описания.\n";
    }
}


    
    
