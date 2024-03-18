using System.Text.RegularExpressions;
namespace AstroHelper2;

public class CleaningHome
{
    public void SimplifyTableHome()
    {
        string sourceFilePath = "DATA1.xml"; 
        string destinationFilePath = "Home.xml"; 
        string startWithPhrase1 = " Дома  Долгота"; 
        
        var lines = File.ReadAllLines(sourceFilePath);

        var updatedLines = lines
            .Where(line => line.StartsWith(startWithPhrase1))
            .Select(line => line
                    
                .Replace("&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;", "") 
                .Replace(" Дома  Долгота", "") 
                .Replace(" Дом", "") 
                .Replace("&amp;deg;", "°")
                .Replace(" узел", "узел")
                .Replace("Развернуть/Свернуть", "")) 
            .Select(line => Regex.Replace(line, "[a-zA-Z]", "")) 
            .Select(line => Regex.Replace(line, @"\d+°\d+'\d+\""", "")) 
            .Select(line => Regex.Replace(line, @"[^0-9\sа-яА-Яa-zA-Z]", ""))
            .Select(line => Regex.Replace(line, @"(\d)й\b", "$1"))
            .ToList();

        
        File.WriteAllLines(destinationFilePath, updatedLines);
    }
}