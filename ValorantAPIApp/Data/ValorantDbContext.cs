using Microsoft.EntityFrameworkCore;
using ValorantAPIApp.Models;

namespace ValorantAPIApp.Data
{
    public class ValorantDbContext : DbContext
    {
        public ValorantDbContext(DbContextOptions<ValorantDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PlayerWeapon>()
                .HasKey(c => new { c.PlayerId, c.WeaponUuid });
            modelBuilder.Entity<AgentLoadout>()
                .HasKey(c => new { c.PlayerId, c.AgentUuid });
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<AgentLoadout> AgentLoadouts { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<PlayerWeapon> PlayerWeapons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<WeaponSkin> WeaponSkins { get; set; }
    }
}
