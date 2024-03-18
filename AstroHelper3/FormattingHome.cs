using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace AstroHelper2
{
    public class FormattingHome
    {
        private string filePath;

        public FormattingHome(string filePath)
        {
            this.filePath = filePath;
        }

        public void ProcessFileHome()
        {
            string data = File.ReadAllText(filePath);
            string pattern = @"(\d+)\s+([^\d]+)";
            MatchCollection matches = Regex.Matches(data, pattern);

            using (var context = new AppDBContext())
            {
                // Очистка таблицы
                context.Database.ExecuteSqlRaw("DELETE FROM Homes");

                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        string home = match.Groups[1].Value;
                        string position = match.Groups[2].Value;

                        context.HomeDbs.AddRange(home, position);
                    }
                }

                context.SaveChanges();
            }
        }
    }
}