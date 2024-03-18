using System.Text.RegularExpressions;
       

namespace AstroHelper3
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
                // Очистка таблицы PlanetsDB
                var allPlanets = context.PlanetsDbs.ToList();
                context.PlanetsDbs.RemoveRange(allPlanets);
                
                foreach (Match match in matches)
                {
                            
                    string planets = match.Groups[1].Value.Trim();
                    string position = match.Groups[2].Value.Trim();
                    int home = int.Parse(match.Groups[3].Value); 
                            
                    var entry = new PlanetsDB{Planets = planets, Home = home, Position = position};
                    context.Update(entry);
                            
                }

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    // Возможно, здесь будет полезно логировать полный стек вызовов или внутренние исключения
                }
            }
        }
    }
}