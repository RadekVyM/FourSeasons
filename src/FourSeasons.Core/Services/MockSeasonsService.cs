using FourSeasons.Core.Interfaces;
using FourSeasons.Core.Models;

namespace FourSeasons.Core.Services;

public class MockSeasonsService : ISeasonsService
{
    public async Task<IList<Season>> GetSeasons()
    {
        await Task.CompletedTask;
        var seasons = CreateMockData();
        return seasons.ToList();
    }

    private static List<Season> CreateMockData()
    {
        var skiGuid = Guid.NewGuid();
        var festivalGuid = Guid.NewGuid();
        var hikingGuid = Guid.NewGuid();

        var summerGuid = Guid.NewGuid();
        var autumnGuid = Guid.NewGuid();
        var winterGuid = Guid.NewGuid();
        var rainyGuid = Guid.NewGuid();

        var location = new Location
        {
            Id = Guid.NewGuid(),
            Area = "Asalem - Khalkhal",
            ShortArea = "Khalkhal",
            Country = "Iran",
        };

        List<Attraction> attractions =
        [
            new Attraction
            {
                Id = skiGuid,
                Image = "ski.png",
                Name = "Ski",
            },
            new Attraction
            {
                Id = festivalGuid,
                Image = "festival.png",
                Name = "Festival",
            },
            new Attraction
            {
                Id = hikingGuid,
                Image = "hiking.png",
                Name = "Hiking",
            }
        ];

        List<Season> seasons = 
        [
            new Season
            {
                Id = summerGuid,
                Name = "Summer",
                Icon = "summer_icon.png",
                Image = "summer.jpg",
                LightImage = true,
                Details = "enjoy the beauty of summer with various attractions available here, as well as various complete facilities available at affordable prices for all tourists from all over the world",
                Location = location,
                Attractions =
                [
                    attractions[0],
                    attractions[1],
                    attractions[2],
                ]
            },
            new Season
            {
                Id = winterGuid,
                Name = "Winter",
                Icon = "winter_icon.png",
                Image = "winter.png",
                LightImage = true,
                Details = "enjoy the beauty of winter with various attractions available here, as well as various complete facilities available at affordable prices for all tourists from all over the world",
                Location = location,
                Attractions =
                [
                    attractions[2],
                    attractions[0],
                    attractions[1],
                ]
            },
            new Season
            {
                Id = autumnGuid,
                Name = "Autumn",
                Icon = "autumn_icon.png",
                Image = "autumn.jpg",
                LightImage = true,
                Details = "enjoy the beauty of autumn with various attractions available here, as well as various complete facilities available at affordable prices for all tourists from all over the world",
                Location = location,
                Attractions =
                [
                    attractions[0],
                    attractions[2],
                    attractions[1],
                ]
            },
            new Season
            {
                Id = rainyGuid,
                Name = "Rainy",
                Icon = "rainy_icon.png",
                Image = "rainy.jpg",
                LightImage = false,
                Details = "enjoy the beauty of rainy with various attractions available here, as well as various complete facilities available at affordable prices for all tourists from all over the world",
                Location = location,
                Attractions =
                [
                    attractions[1],
                    attractions[0],
                    attractions[2],
                ]
            }
        ];

        AddTestingTouristStats(seasons[0]);
        AddTestingTouristStats(seasons[1]);
        AddTestingTouristStats(seasons[2]);
        AddTestingTouristStats(seasons[3]);

        return seasons;
    }

    private static void AddTestingTouristStats(Season season)
    {
        var random = new Random();
        var end = DateTime.Today.Year - random.Next(1, 2);

        for (int year = DateTime.Today.Year; year >= end; year--)
        {
            var touristStats = new TouristStats
            {
                Id = Guid.NewGuid(),
                Year = year,
            };
            touristStats.MonthlyTouristStats = CreateTestingMonthlyTouristStats(touristStats).ToList();

            season.TouristStats.Add(touristStats);
        }
    }

    private static IEnumerable<MonthlyTouristStats> CreateTestingMonthlyTouristStats(TouristStats touristStats)
    {
        var random = new Random();
        var end = DateTime.Today.Year == touristStats.Year ? DateTime.Today.Month : 12;

        for (int month = 1; month <= end; month++)
        {
            yield return new MonthlyTouristStats
            {
                Id = Guid.NewGuid(),
                Stats = (int)Math.Abs(Math.Round(Math.Sin((month / 4d) - 0.1d) * random.Next(100, 200))),
                Month = month
            };
        }
    }
}