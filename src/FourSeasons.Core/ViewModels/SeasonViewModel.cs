namespace FourSeasons.Core.ViewModels
{
    public class SeasonViewModel
    {
        public Guid SeasonId { get; init; }
        public string Name { get; init; }
        public string Image { get; init; }
        public string Icon { get; init; }
        public string Details { get; init; }
        public LocationViewModel Location { get; init; }
        public IReadOnlyList<AttractionViewModel> Attractions { get; init; }
        public IReadOnlyList<TouristStatsViewModel> TouristStats { get; init; }
    }
}
