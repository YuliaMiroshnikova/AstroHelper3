using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace AstroHelper3;

public class UpravitelInHomeDescription
{
    private AppDBContext _context;
    private string _outputFilePath;
    private string _jsonFilePath;
    

    public UpravitelInHomeDescription(AppDBContext context, string outputFilePath, string jsonFilePath)
    {
        _context = context;
        _outputFilePath = outputFilePath;
        _jsonFilePath = jsonFilePath;
    }

    public List<UpravitelDescription> LoadDescriptions(string jsonFilePath)
    {
        var jsonString = File.ReadAllText(jsonFilePath);
        return JsonSerializer.Deserialize<List<UpravitelDescription>>(jsonString) ?? new List<UpravitelDescription>();
    }

    public void GenerateDescriptionsUpraviteltHome()
    {


        var descriptions = LoadDescriptions(_jsonFilePath);
        var homeDbs = _context.HomeDbs.ToList();
        var planetsDbs = _context.PlanetsDbs.ToList();

        var rulerToPlanetMapping = new Dictionary<string, string>
        {
            { "Водолей", "Уран" },
            { "Рыбы", "Нептун" },
            { "Овен", "Марс" },
            { "Телец", "Венера" },
            { "Близнецы", "Меркурий" },
            { "Рак", "Луна" },
            { "Лев", "Солнце" },
            { "Дева", "Меркурий" },
            { "Весы", "Венера" },
            { "Скорпион", "Плутон" },
            { "Стрелец", "Юпитер" },
            { "Козерог", "Сатурн" }
        };

        using (var writer = new StreamWriter(_outputFilePath, true))
        {
            foreach (var homeDb in homeDbs)
            {
                if (rulerToPlanetMapping.TryGetValue(homeDb.Position, out var planetName))
                {
                   
                    var planetHomeNumber = planetsDbs.FirstOrDefault(p => p.Planets == planetName)?.Home;

                    if (planetHomeNumber != null)
                    {
                        
                        var description = descriptions.FirstOrDefault(d => 
                            d.Home == homeDb.Home && d.UpravitelPosition == planetHomeNumber)?.Description 
                                          ?? "Описание отсутствует.";

                        writer.WriteLine($"Дом {homeDb.Home} ({homeDb.Position}), управитель - {planetName}" +
                                         $"в доме {planetHomeNumber}.\n " +
                                         $"Описание: {description}\n");
                    }
                    }
                    else
                    {
                        writer.WriteLine($"Для знака зодиака {homeDb.Position} управляющая планета {planetName} не найдена в PlanetsDbs.");
                    }
                
            }
        }
        
    }
    
}

