using FourSeasons.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FourSeasons.Data
{
    internal static class MockExtensions
    {
        public static void AddMockingData(this ModelBuilder builder)
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

            builder.Entity<Location>().HasData(location);

            builder.Entity<Attraction>().HasData(
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
                });

            builder.Entity<Season>().HasData(
                new Season
                {
                    Id = summerGuid,
                    Name = "Summer",
                    Icon = "summer_icon.png",
                    Image = "summer.jpg",
                    LightImage = true,
                    Details = "enjoy the beauty of summer with various attractions available here, as well as various complete facilities available at affordable prices for all tourists from all over the world",
                    LocationId = location.Id
                },
                new Season
                {
                    Id = winterGuid,
                    Name = "Winter",
                    Icon = "winter_icon.png",
                    Image = "winter.png",
                    LightImage = true,
                    Details = "enjoy the beauty of winter with various attractions available here, as well as various complete facilities available at affordable prices for all tourists from all over the world",
                    LocationId = location.Id
                },
                new Season
                {
                    Id = autumnGuid,
                    Name = "Autumn",
                    Icon = "autumn_icon.png",
                    Image = "autumn.jpg",
                    LightImage = true,
                    Details = "enjoy the beauty of autumn with various attractions available here, as well as various complete facilities available at affordable prices for all tourists from all over the world",
                    LocationId = location.Id
                },
                new Season
                {
                    Id = rainyGuid,
                    Name = "Rainy",
                    Icon = "rainy_icon.png",
                    Image = "rainy.jpg",
                    LightImage = false,
                    Details = "enjoy the beauty of rainy with various attractions available here, as well as various complete facilities available at affordable prices for all tourists from all over the world",
                    LocationId = location.Id
                });

            builder.Entity<SeasonAttraction>().HasData(
                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = summerGuid,
                    AttractionId = skiGuid,
                },
                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = summerGuid,
                    AttractionId = festivalGuid,
                },
                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = summerGuid,
                    AttractionId = hikingGuid,
                },

                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = autumnGuid,
                    AttractionId = skiGuid,
                },
                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = autumnGuid,
                    AttractionId = hikingGuid,
                },
                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = autumnGuid,
                    AttractionId = festivalGuid,
                },

                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = winterGuid,
                    AttractionId = hikingGuid,
                },
                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = winterGuid,
                    AttractionId = skiGuid,
                },
                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = winterGuid,
                    AttractionId = festivalGuid,
                },

                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = rainyGuid,
                    AttractionId = festivalGuid,
                },
                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = rainyGuid,
                    AttractionId = skiGuid,
                },
                new SeasonAttraction
                {
                    Id = Guid.NewGuid(),
                    SeasonId = rainyGuid,
                    AttractionId = hikingGuid,
                });

            builder.AddTestingTouristStats(summerGuid);
            builder.AddTestingTouristStats(winterGuid);
            builder.AddTestingTouristStats(autumnGuid);
            builder.AddTestingTouristStats(rainyGuid);
        }

        private static void AddTestingTouristStats(this ModelBuilder builder, Guid seasonId)
        {
            var random = new Random();
            var end = DateTime.Today.Year - random.Next(1, 2);

            for (int year = DateTime.Today.Year; year >= end; year--)
            {
                var touristStats = new TouristStats
                {
                    Id = Guid.NewGuid(),
                    SeasonId = seasonId,
                    Year = year
                };

                builder.Entity<MonthlyTouristStats>()
                    .HasData(CreateTestingMonthlyTouristStats(touristStats));
                builder.Entity<TouristStats>()
                    .HasData(touristStats);
            }
        }

        private static IEnumerable<MonthlyTouristStats> CreateTestingMonthlyTouristStats(TouristStats touristStats)
        {
            int end = DateTime.Today.Year == touristStats.Year ? DateTime.Today.Month : 12;

            var random = new Random();

            for (int month = 1; month <= end; month++)
            {
                yield return new MonthlyTouristStats
                {
                    Id = Guid.NewGuid(),
                    Stats = (int)Math.Abs(Math.Round(Math.Sin((month / 4d) - 0.1d) * random.Next(100, 200))),
                    TouristStatsId = touristStats.Id,
                    Month = month
                };
            }
        }
    }
}
