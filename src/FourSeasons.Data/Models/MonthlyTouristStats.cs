﻿namespace FourSeasons.Data.Models
{
    // I have no idea how these stats should work 😶
    public class MonthlyTouristStats
    {
        public Guid Id { get; set; }
        public Guid? TouristStatsId { get; set; }
        public virtual TouristStats TouristStats { get; set; }
        public int Stats { get; set; }
        public int Month { get; set; }
    }
}
