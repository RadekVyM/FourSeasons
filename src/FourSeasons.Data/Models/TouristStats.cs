namespace FourSeasons.Data.Models
{
    // I have no idea how these stats should work 😶
    public class TouristStats
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public Guid? SeasonId { get; set; }
        public virtual Season Season { get; set; }
        public virtual List<MonthlyTouristStats> MonthlyTouristStats { get; set; }
    }
}
