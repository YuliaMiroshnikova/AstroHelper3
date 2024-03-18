namespace AstroHelper2;

public class HomeDB
{
    
        public string? home { get; set; }
        public string? position { get; set; }
    
        public HomeDB() { }

        public HomeDB(string home, string position)
        {
            this.home = home;
            this.position = position;
        }
    
}