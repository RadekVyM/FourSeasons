using FourSeasons.Core.Interfaces;
using FourSeasons.Core.Services;
using FourSeasons.Core.ViewModels;
using FourSeasons.Maui.Views.Pages;

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

		builder.Services.AddSingleton<ISeasonsService, MockSeasonsService>();

        builder.Services.AddTransient<SeasonsPage>();
		builder.Services.AddTransient<ISeasonsPageViewModel, SeasonsPageViewModel>();

        var app = builder.Build();

		return app;
	}
}