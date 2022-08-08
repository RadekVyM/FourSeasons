using FourSeasons.Data.Models;

namespace FourSeasons.Data.Interfaces
{
    public interface ISeasonsRepository
    {
        Task<IEnumerable<Season>> GetAll();
    }
}
