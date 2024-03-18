namespace AstroHelper2;

public class PlanetsDB
{
    
    public string? planets { get; set; }
    public string? position { get; set; }
    public string? home { get; set; }
    
    public PlanetsDB() { }

    public PlanetsDB(string planets, string position, string home)
    {
        this.planets = planets;
        this.position = position;
        this.home = home;
    }
    
}