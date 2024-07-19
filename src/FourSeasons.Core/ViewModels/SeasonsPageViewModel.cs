using FourSeasons.Core.Interfaces;

namespace FourSeasons.Core.ViewModels;

public class SeasonsPageViewModel(ISeasonsService seasonsService) : BasePageViewModel, ISeasonsPageViewModel
{
    private bool initialAppearing = true;
    private readonly ISeasonsService seasonsService = seasonsService;

    private SeasonViewModel season;
    private IReadOnlyList<SeasonViewModel> seasons = [];

    public SeasonViewModel CurrentSeason
    {
        get => season;
        set
        {
            season = value;
            season.Distance = GetDefaultDistanceForSeason(season);
            OnPropertyChanged(nameof(CurrentSeason));
        }
    }

    public IReadOnlyList<SeasonViewModel> Seasons 
    { 
        get => seasons; 
        private set
        {
            seasons = value;
            OnPropertyChanged(nameof(Seasons));
        }
    }


    public override async Task OnAppearing()
    {
        if (initialAppearing)
        {
            await LoadSeasons();
            CurrentSeason = Seasons.FirstOrDefault();
        }

        await base.OnAppearing();

        initialAppearing = false;
    }

    private async Task LoadSeasons()
    {
        var seasons = await seasonsService.GetSeasons();
        int number = 0;

        // TODO: Better mapping?
        Seasons = seasons.Select(s =>
        {
            return new SeasonViewModel
            {
                SeasonId = s.Id,
                Details = s.Details,
                Icon = s.Icon,
                Image = s.Image,
                LightImage = s.LightImage,
                Name = s.Name,
                Number = ++number,
                Location = new LocationViewModel
                {
                    LocationId = s.Location.Id,
                    Area = s.Location.Area,
                    ShortArea = s.Location.ShortArea,
                    Country = s.Location.Country
                },
                Attractions = s.Attractions.Select(a =>
                {
                    return new AttractionViewModel
                    {
                        AttractionId = a.Id,
                        Image = a.Image,
                        Name = a.Name
                    };
                }).Take(3).ToList(),
                TouristStats = s.TouristStats.Select(t =>
                {
                    return new TouristStatsViewModel
                    {
                        TouristStatsId = t.Id,
                        Year = t.Year,
                        MonthlyTouristStats = t.MonthlyTouristStats.Select(mt =>
                        {
                            return new MonthlyTouristStatsViewModel
                            {
                                MonthlyTouristStatsId = mt.Id,
                                Month = mt.Month,
                                Stats = mt.Stats
                            };
                        })
                        .OrderBy(m => m.Month)
                        .ToList()
                    };
                })
                .OrderByDescending(t => t.Year)
                .ToList()
            };
        }).ToList();
    }

    private static DistanceViewModel GetDefaultDistanceForSeason(SeasonViewModel season) =>
        new()
        {
            CurrentLocation = "Prague",
            DestinationLocation = season.Location.ShortArea,
            Distance = 4002,
            TypeOfTransport = TypeOfTransport.Car
        };
}