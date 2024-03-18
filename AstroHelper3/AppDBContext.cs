using Microsoft.EntityFrameworkCore;
namespace AstroHelper2;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }
    
    public DbSet<string> HomeDbs { get; set; } = null!;
    public DbSet<PlanetsDB> PlanetDbs { get; set; } = null!;
    
    private const string HOST = "localhost";
    private const string PORT = "8889"; // 3306
    private const string DATABASE = "astro"; 
    private const string USER = "root"; // mysql
    private const string PASS = "root"; // ""
    
    public AppDBContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseMySql($"Server={HOST};Database={DATABASE};Port={PORT};User={USER};Password={PASS}", new MySqlServerVersion(new Version(8, 0, 0)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HomeDB>();
    }
}