using Microsoft.EntityFrameworkCore;
namespace AstroHelper3;

public class AppDBContext : DbContext
{

    public DbSet<HomeDB> HomeDbs { get; set; } = null!;
    public DbSet<PlanetsDB> PlanetsDbs { get; set; } = null!;
    
    private const string HOST = "localhost";
    private const string PORT = "8889"; // 3306
    private const string DATABASE = "astro"; 
    private const string USER = "root"; // mysql
    private const string PASS = "root"; // ""
    
    public AppDBContext()
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseMySql($"Server={HOST};Database={DATABASE};Port={PORT};User={USER};Password={PASS}", new MySqlServerVersion(new Version(8, 0, 0)));
    }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<HomeDB>();
    // }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlanetsDB>()
            .HasOne(p => p.HomeNavigation) 
            .WithMany(h => h.Planets) 
            .HasForeignKey(p => p.Home); 
    }
}