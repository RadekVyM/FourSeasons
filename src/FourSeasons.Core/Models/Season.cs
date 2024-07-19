namespace FourSeasons.Core.Models;

public class Season
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public bool LightImage { get; set; }
    public string Icon { get; set; }
    public Location Location { get; set; }
    public string Details { get; set; }
    public List<Attraction> Attractions { get; set; } = [];
    public List<TouristStats> TouristStats { get; set; } = [];
}