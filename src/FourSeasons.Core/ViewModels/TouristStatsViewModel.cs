namespace FourSeasons.Core.ViewModels
{
    public class TouristStatsViewModel : ObservableObject
    {
        public Guid TouristStatsId { get; set; }
        public int Year { get; set; }
        public virtual IReadOnlyList<MonthlyTouristStatsViewModel> MonthlyTouristStats { get; init; }
        public int Trend
        {
            get
            {
                if (MonthlyTouristStats.Count <= 1)
                    return 1;
                else
                    return MonthlyTouristStats[MonthlyTouristStats.Count - 1].Stats - MonthlyTouristStats[MonthlyTouristStats.Count - 2].Stats < 0 ? -1 : 1;
            }
        }
    }
}
