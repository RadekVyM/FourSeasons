using FourSeasons.Core.Interfaces;

namespace FourSeasons.Maui.Views.Pages;

public partial class SeasonsPage : ContentPage
{
	private readonly ISeasonsPageViewModel seasonsPageViewModel;

	public SeasonsPage(ISeasonsPageViewModel seasonsPageViewModel)
	{
		InitializeComponent();
		BindingContext = this.seasonsPageViewModel = seasonsPageViewModel;
	}

	protected override async void OnAppearing()
	{
        base.OnAppearing();
		await seasonsPageViewModel.OnAppearing();
	}

	protected override async void OnDisappearing()
	{
		base.OnDisappearing();
		await seasonsPageViewModel.OnDisappearing();
	}
}