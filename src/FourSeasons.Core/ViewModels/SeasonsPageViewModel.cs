﻿using FourSeasons.Core.Interfaces;

namespace FourSeasons.Core.ViewModels
{
    public class SeasonsPageViewModel : BasePageViewModel, ISeasonsPageViewModel
    {
        private readonly ISeasonsService seasonsService;

        private IReadOnlyList<SeasonViewModel> seasons;

        public IReadOnlyList<SeasonViewModel> Seasons 
        { 
            get => seasons; 
            private set
            {
                seasons = value;
                OnPropertyChanged(nameof(Seasons));
            }
        }

        public SeasonsPageViewModel(ISeasonsService seasonsService)
        {
            this.seasonsService = seasonsService;
        }

        public override async Task OnAppearing()
        {
            await LoadSeasons();

            await base.OnAppearing();
        }

        private async Task LoadSeasons()
        {
            var seasons = await seasonsService.GetSeasons();

            Seasons = seasons.Select(s =>
            {
                return new SeasonViewModel
                {
                    SeasonId = s.Id,
                    Details = s.Details,
                    Icon = s.Icon,
                    Image = s.Image,
                    Name = s.Name,
                    Location = s.LocationId is null ? null : new LocationViewModel
                    {
                        LocationId = s.Location.Id,
                        Area = s.Location.Area,
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
                    }).ToList(),
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
                            }).ToList()
                        };
                    }).ToList()
                };
            }).ToList();
        }
    }
}
