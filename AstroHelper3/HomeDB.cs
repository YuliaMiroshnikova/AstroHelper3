using System.ComponentModel.DataAnnotations;

namespace AstroHelper3;

public class HomeDB
{
    
    [Key]
    public int Home { get; set; } // Первичный ключ
    public string? Position { get; set; }
    
    public ICollection<PlanetsDB> Planets { get; set; } = new List<PlanetsDB>();
        public HomeDB() { }

        public HomeDB(int home, string position)
        {
            this.Home = home;
            this.Position = position;
        }
    
}