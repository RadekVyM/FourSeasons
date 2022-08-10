using FourSeasons.Core.Interfaces;
#if WINDOWS
using FourSeasons.Maui.Behaviors;
#endif

namespace FourSeasons.Maui.Views.Pages;

public partial class SeasonsPage : ContentPage
{
	private readonly ISeasonsPageViewModel seasonsPageViewModel;

	public SeasonsPage(ISeasonsPageViewModel seasonsPageViewModel)
	{
		InitializeComponent();
		BindingContext = this.seasonsPageViewModel = seasonsPageViewModel;

		Loaded += SeasonsPageLoaded;
		Unloaded += SeasonsPageUnloaded;
		SizeChanged += SeasonsPageSizeChanged;
	}

	private void SeasonsPageSizeChanged(object sender, EventArgs e)
	{
		var wide = "Wide";
		var narrow = "Narrow";

        VisualStateManager.GoToState(this, Width < 1000 ? narrow : wide);
	}

	private void SeasonsPageLoaded(object sender, EventArgs e)
	{
#if WINDOWS
        image.Behaviors.Add(new CenteredImageBehavior());
#endif
    }

    private void SeasonsPageUnloaded(object sender, EventArgs e)
    {
        Loaded -= SeasonsPageLoaded;
        Unloaded -= SeasonsPageUnloaded;
        SizeChanged -= SeasonsPageSizeChanged;
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