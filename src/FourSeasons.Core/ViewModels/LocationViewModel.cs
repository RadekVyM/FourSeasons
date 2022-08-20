namespace FourSeasons.Core.ViewModels
{
    public class LocationViewModel : ObservableObject
    {
        public Guid LocationId { get; init; }
        public string Country { get; init; }
        public string Area { get; init; }
        public string ShortArea { get; init; }
        public string CombinedAreaCountry => $"{Area}, {Country}";
    }
}
