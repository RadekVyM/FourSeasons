using FourSeasons.Core.Interfaces;
using FourSeasons.Core.Services;
using FourSeasons.Core.ViewModels;
using FourSeasons.Data;
using FourSeasons.Data.Interfaces;
using FourSeasons.Data.Repositories;
using FourSeasons.Maui.Views.Pages;
using SQLitePCL;

namespace FourSeasons.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                fonts.AddFont("Poppins-Semibold.ttf", "PoppinsSemibold");
                fonts.AddFont("Vertigup.ttf", "Vertigup");
                fonts.AddFont("SQUEMB.ttf", "SQUEMB");
            });

		builder.Services.AddDbContextFactory<ApplicationDbContext>();
		builder.Services.AddSingleton<ISeasonsRepository, SeasonsRepository>();

		builder.Services.AddSingleton<ISeasonsService, SeasonsService>();

        builder.Services.AddTransient<SeasonsPage>();
		builder.Services.AddTransient<ISeasonsPageViewModel, SeasonsPageViewModel>();

        return builder.Build();
	}
}
