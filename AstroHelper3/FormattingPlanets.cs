
        // string data = File.ReadAllText(filePath);
        // string pattern = @"(\w+)\s+(\w+)\s+(\d{1,2})";
        // MatchCollection matches = Regex.Matches(data, pattern);

        using System;
        using System.IO;
        using System.Text.RegularExpressions;
        using Microsoft.EntityFrameworkCore;

        namespace AstroHelper2
        {
            public class FormattingPlanets
            {
                private string filePath;

                public FormattingPlanets(string filePath)
                {
                    this.filePath = filePath;
                }

                public void ProcessFilePlanets()
                {
                    string data = File.ReadAllText(filePath);
                    string pattern = @"(\w+)\s+(\w+)\s+(\d{1,2})";
                    MatchCollection matches = Regex.Matches(data, pattern);

                    using (var context = new AppDBContext())
                    {
                        // Очистка таблицы
                        context.Database.ExecuteSqlRaw("DELETE FROM Planets");

                        foreach (Match match in matches)
                        {
                            if (match.Success)
                            {
                                string planets = match.Groups[1].Value;
                                string position = match.Groups[2].Value;
                                string home = match.Groups[3].Value; 
                                context.HomeDbs.AddRange(planets, home, position);
                            }
                        }

                        context.SaveChanges();
                    }
                }
            }
        }