using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameRental.Models
{
  public class GameRentalContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Developer> Developers { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Copy> Copies { get; set; }
    public DbSet<DeveloperGame> DeveloperGame { get; set; }

    public GameRentalContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}