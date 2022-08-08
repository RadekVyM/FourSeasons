using FourSeasons.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FourSeasons.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MonthlyTouristStats> MonthlyTouristStats { get; set; }
        public DbSet<TouristStats> TouristStats { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<SeasonAttraction> SeasonAttractions { get; set; }
        public DbSet<Location> Locations { get; set; }

        public string DbPath { get; }

        public ApplicationDbContext()
        {
            SQLitePCL.Batteries_V2.Init();
           
            DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "fourseasons.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MonthlyTouristStats>()
                .HasKey(x => x.Id);

            builder.Entity<TouristStats>()
                .HasKey(x => x.Id);

            builder.Entity<Season>()
                .HasKey(x => x.Id);

            builder.Entity<Attraction>()
                .HasKey(x => x.Id);

            builder.Entity<Location>()
                .HasKey(x => x.Id);

            builder.Entity<TouristStats>()
                .HasMany(ts => ts.MonthlyTouristStats)
                .WithOne(mts => mts.TouristStats)
                .HasForeignKey(mts => mts.TouristStatsId);

            builder.Entity<Location>()
                .HasMany(l => l.Seasons)
                .WithOne(s => s.Location)
                .HasForeignKey(s => s.LocationId);

            builder.Entity<Season>()
                .HasMany(s => s.Attractions)
                .WithMany(a => a.Seasons)
                .UsingEntity<SeasonAttraction>();

            builder.Entity<Season>()
                .HasMany(s => s.TouristStats)
                .WithOne(ts => ts.Season)
                .HasForeignKey(ts => ts.SeasonId);

            builder.AddMockingData();
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
