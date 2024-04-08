using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AstroHelper3;

public class KarmicNodesProcessor
{
    private AppDBContext _context;
    private string _outputFilePath;
    private string _jsonFilePathHomes;
    private string _jsonFilePathPositions;
    

    public KarmicNodesProcessor(AppDBContext context, string outputFilePath, string jsonFilePathHomes, string jsonFilePathPositions)
    {
        _context = context;
        _outputFilePath = outputFilePath;
        _jsonFilePathHomes = jsonFilePathHomes;
        _jsonFilePathPositions = jsonFilePathPositions;
    }

    public void ProcessKarmicNodes()
    {
        var karmicNodeDescriptions = LoadKarmicNodeDescriptions(_jsonFilePathHomes);
        var karmicPositionDescriptions = LoadKarmicPositionDescriptions(_jsonFilePathPositions);
        var planetsDbs = _context.PlanetsDbs.ToList();

        var northNode = planetsDbs.FirstOrDefault(p => p.Planets == "Восхузел");
        var southNode = planetsDbs.FirstOrDefault(p => p.Planets == "Низхузел");

        using (var writer = new StreamWriter(_outputFilePath, true))
        {
            
            if (northNode != null && southNode != null)
            {
                foreach (var description in karmicNodeDescriptions)
                {
                    if (description.HomeNorth == northNode.Home && description.HomeSouth == southNode.Home)
                    {
                        writer.WriteLine($"Раху (Дом: {northNode.Home}),\n" +
                                         $"Кету (Дом: {southNode.Home}). \n " +
                                         $"Описание: {description.Description}\n");
                        
                    }
                }
            }

           
            if (northNode != null && southNode != null)
            { 
                foreach (var description in karmicPositionDescriptions)
                {
                    if (description.PositionNorth == northNode.Position && description.PositionSouth == southNode.Position)
                    {
                        writer.WriteLine($"\n\nРаху (Знак: {northNode.Position}),\n" +
                                         $"Кету (Знак: {southNode.Position}).\n" +
                                         $"{description.Description}\n");
                       
                    }
                }
            }
        }
    }

    private List<KarmicNodeDescription> LoadKarmicNodeDescriptions(string jsonFilePath)
    {
        var jsonString = File.ReadAllText(jsonFilePath);
        return JsonSerializer.Deserialize<List<KarmicNodeDescription>>(jsonString) ?? new List<KarmicNodeDescription>();
    }

    private List<KarmicPositionDescription> LoadKarmicPositionDescriptions(string jsonFilePath)
    {
        var jsonString = File.ReadAllText(jsonFilePath);
        return JsonSerializer.Deserialize<List<KarmicPositionDescription>>(jsonString) ?? new List<KarmicPositionDescription>();
    }
}


