using FourSeasons.Core.Models;

namespace FourSeasons.Core.Interfaces;

public interface ISeasonsService
{
    Task<IList<Season>> GetSeasons();
}