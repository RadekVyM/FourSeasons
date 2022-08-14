using FourSeasons.Core.Interfaces;
#if WINDOWS
using FourSeasons.Maui.Behaviors;
#endif

namespace FourSeasons.Maui.Views.Pages;

public partial class SeasonsPage : ContentPage
{
	const string WideState = "Wide";
	const string NarrowState = "Narrow";

	private double stateBreakpoint => 1000;
	private double detailsHeaderHeight => 80;
    private double detailsShownPosition => 0;
	private double detailsHiddenPosition = 0;
    private double lastDetailsTranslation = 0;

    private string currentState = "";
    private bool isDetailsRootContainerOpen = false;

    private readonly ISeasonsPageViewModel seasonsPageViewModel;


	public SeasonsPage(ISeasonsPageViewModel seasonsPageViewModel)
	{
		InitializeComponent();

        BindingContext = this.seasonsPageViewModel = seasonsPageViewModel;

		Loaded += SeasonsPageLoaded;
		Unloaded += SeasonsPageUnloaded;
		SizeChanged += SeasonsPageSizeChanged;
	}


    private void UpdateState(double width)
    {
        var isNarrow = width < stateBreakpoint;
        var newState = isNarrow ? NarrowState : WideState;

        if (currentState == newState)
            return;

        currentState = newState;

        VisualStateManager.GoToState(this, currentState);

        if (isNarrow)
            isDetailsRootContainerOpen = false;
    }

    private async Task HideDetails()
    {
        await DetailsRootContainer.TranslateTo(0, detailsHiddenPosition, length: 250);
        isDetailsRootContainerOpen = false;
    }

    private async Task ShowDetails()
    {
        await DetailsRootContainer.TranslateTo(0, detailsShownPosition, length: 250);
        isDetailsRootContainerOpen = true;
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

    private void SeasonsPageSizeChanged(object sender, EventArgs e)
    {
        UpdateState(Width);
    }

    private void DetailsRootContainerSizeChanged(object sender, EventArgs e)
    {
        if (!isDetailsRootContainerOpen && currentState == NarrowState)
        {
            detailsHiddenPosition = lastDetailsTranslation = DetailsRootContainer.TranslationY = DetailsRootContainer.Height - detailsHeaderHeight;
        }
    }

    private async void DetailsPanUpdated(object sender, PanUpdatedEventArgs e)
	{
        switch (e.StatusType)
        {
            case GestureStatus.Running:
                lastDetailsTranslation = DetailsRootContainer.TranslationY + e.TotalY;
                await DetailsRootContainer.TranslateTo(0, Math.Min(Math.Max(lastDetailsTranslation, detailsShownPosition), detailsHiddenPosition), length: 50);
                break;
            case GestureStatus.Canceled:
            case GestureStatus.Completed:
                if ((!isDetailsRootContainerOpen && lastDetailsTranslation < detailsHiddenPosition - 40) || (isDetailsRootContainerOpen && lastDetailsTranslation <= 40))
                    await ShowDetails();
                else
                    await HideDetails();
                break;
        }
    }

    private async void ShowButtonClicked(object sender, EventArgs e)
    {
        await ShowDetails();
    }
}