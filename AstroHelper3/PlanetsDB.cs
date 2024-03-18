using System.ComponentModel.DataAnnotations;

namespace AstroHelper3;

public class PlanetsDB
{
  
    [Key]
    public int Id { get; set; } // Первичный ключ
    public string? Planets { get; set; }
    public string? Position { get; set; }
    public int Home { get; set; } // Внешний ключ, ссылается на HomeDB
    public HomeDB? HomeNavigation { get; set; }
    public PlanetsDB() { }

    public PlanetsDB(string planets, string position, int home)
    {
        this.Planets = planets;
        this.Position = position;
        this.Home = home;
    }
    
}