using FourSeasons.Data.Interfaces;
using FourSeasons.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FourSeasons.Data.Repositories
{
    public class SeasonsRepository : ISeasonsRepository
    {
        readonly IDbContextFactory<ApplicationDbContext> contextFactory;

        public SeasonsRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Season>> GetAll()
        {
            using (var dbContext = contextFactory.CreateDbContext())
            {
                dbContext.Database.EnsureCreated(); // This is probably bad

                var seasons = dbContext.Seasons
                    .Include(s => s.TouristStats)
                    .ThenInclude(ts => ts.MonthlyTouristStats)
                    .Include(s => s.Location)
                    .Include(s => s.Attractions);

                return await seasons.ToListAsync();
            }
        }
    }
}
