namespace FourSeasons.Core.ViewModels
{
    public class MonthlyTouristStatsViewModel : ObservableObject
    {
        public Guid MonthlyTouristStatsId { get; init; }
        public int Stats { get; init; }
        public int Month { get; init; }
    }
}
