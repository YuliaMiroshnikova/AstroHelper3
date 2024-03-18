using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace AstroHelper3
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
                foreach (Match match in matches)
                {
                    int home = int.Parse(match.Groups[1].Value);
                    string position = match.Groups[2].Value.Trim();

                    var entry = new HomeDB{Home = home, Position = position};
                    context.Update(entry);
                }

                context.SaveChanges();
            }
        }
    }
}