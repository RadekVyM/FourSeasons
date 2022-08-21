using FourSeasons.Core.ViewModels;

namespace FourSeasons.Core.Interfaces
{
    public interface ISeasonsPageViewModel : IBasePageViewModel
    {
        SeasonViewModel CurrentSeason { get; set; }
        IReadOnlyList<SeasonViewModel> Seasons { get; }
    }
}
