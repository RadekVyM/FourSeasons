namespace FourSeasons.Core.ViewModels
{
    public enum TypeOfTransport
    {
        Car
    }

    public class DistanceViewModel : ObservableObject
    {
        public TypeOfTransport TypeOfTransport { get; set; }
        public string CurrentLocation { get; set; }
        public string DestinationLocation { get; set; }
        public double Distance { get; set; }
    }
}
