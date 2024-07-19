namespace FourSeasons.Core.ViewModels;

public class AttractionViewModel : ObservableObject
{
    public Guid AttractionId { get; init; }
    public string Name { get; init; }
    public string Image { get; init; }
}