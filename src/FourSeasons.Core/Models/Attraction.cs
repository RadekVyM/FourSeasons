namespace FourSeasons.Core.Models;

public class Attraction
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public virtual List<Season> Seasons { get; set; }
}