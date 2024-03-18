using System.Text.RegularExpressions;

namespace AstroHelper2;

public class CleaningPlanets
{
    public void SimplifyTablePlanets()
    {
        string sourceFilePath = "DATA1.xml"; 
        string destinationFilePath = "Planets.xml"; 
        string startWithPhrase1 = " Планеты  Долгота Позиция"; 
        
        var lines = File.ReadAllLines(sourceFilePath);

        var updatedLines = lines
            .Where(line => line.StartsWith(startWithPhrase1))
            .Select(line => line
                    
                .Replace("&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;", "") 
                .Replace(" Планеты  Долгота ПозицияДома", "") 
                .Replace("&amp;deg;", "°")
                .Replace(" узел", "узел")
                .Replace("Развернуть/Свернуть", "")) 
            .Select(line => Regex.Replace(line, "[a-zA-Z]", "")) 
            .Select(line => Regex.Replace(line, @"\d+°\d+'\d+\""", "")) 
            .Select(line => Regex.Replace(line, @"[^0-9\sа-яА-Яa-zA-Z]", "")) 
            .ToList();

        
        File.WriteAllLines(destinationFilePath, updatedLines);
    }
}