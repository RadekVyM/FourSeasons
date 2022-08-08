namespace FourSeasons.Data.Models
{
    public class Season
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public Guid? LocationId { get; set; }
        public virtual Location Location { get; set; }
        public string Details { get; set; }
        public virtual List<Attraction> Attractions { get; set; }
        public virtual List<TouristStats> TouristStats { get; set; }
    }
}
