using FourSeasons.Data.Models;

namespace FourSeasons.Core.Interfaces
{
    public interface ISeasonsService
    {
        Task<IList<Season>> GetSeasons();
    }
}
