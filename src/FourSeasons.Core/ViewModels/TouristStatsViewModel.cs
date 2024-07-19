namespace FourSeasons.Core.ViewModels;

public class TouristStatsViewModel : ObservableObject
{
    public Guid TouristStatsId { get; set; }
    public int Year { get; set; }
    public virtual IReadOnlyList<MonthlyTouristStatsViewModel> MonthlyTouristStats { get; init; }
    public virtual MonthlyTouristStatsViewModel FirstMonthlyTouristStats => MonthlyTouristStats?.FirstOrDefault();
    public int Trend
    {
        get
        {
            if (MonthlyTouristStats is null || MonthlyTouristStats.Count <= 1)
                return 1;
            else
                return MonthlyTouristStats[MonthlyTouristStats.Count - 1].Stats - MonthlyTouristStats[MonthlyTouristStats.Count - 2].Stats < 0 ? -1 : 1;
        }
    }
    public int Summary
    {
        get
        {
            if (MonthlyTouristStats is null || !MonthlyTouristStats.Any())
                return 0;
            else
                return MonthlyTouristStats.Select(m => m.Stats).Sum();
        }
    }
}