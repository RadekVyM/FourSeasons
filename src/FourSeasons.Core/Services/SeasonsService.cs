using FourSeasons.Core.Interfaces;
using FourSeasons.Data.Interfaces;
using FourSeasons.Data.Models;

namespace FourSeasons.Core.Services
{
    public class SeasonsService : ISeasonsService
    {
        private readonly ISeasonsRepository seasonsRepository;

        public SeasonsService(ISeasonsRepository seasonsRepository)
        {
            this.seasonsRepository = seasonsRepository;
        }

        public async Task<IList<Season>> GetSeasons()
        {
            var seasons = await seasonsRepository.GetAll();

            return seasons.ToList();
        }
    }
}
