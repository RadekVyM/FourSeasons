using FourSeasons.Core.Interfaces;
using Microsoft.Maui.Controls.Shapes;
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
    private double wideDetailsRootContainerCornerRadius = 0;
    private double narrowDetailsRootContainerCornerRadius = 24;
    private bool isTouchBased => DeviceInfo.Current.Platform == DevicePlatform.iOS || DeviceInfo.Current.Platform == DevicePlatform.Android || DeviceInfo.Current.Platform == DevicePlatform.watchOS;

    private string currentState = "";
    private bool isDetailsRootContainerOpen = false;

    private readonly ISeasonsPageViewModel seasonsPageViewModel;


	public SeasonsPage(ISeasonsPageViewModel seasonsPageViewModel)
	{
		InitializeComponent();

        UpdateState(0);

        ShowDetailsButton.IsVisible = !isTouchBased;
        SwipeUpDetailsContainer.IsVisible = isTouchBased;

        BindingContext = this.seasonsPageViewModel = seasonsPageViewModel;

		Loaded += SeasonsPageLoaded;
		Unloaded += SeasonsPageUnloaded;
		SizeChanged += SeasonsPageSizeChanged;
	}


    private void UpdateState(double width)
    {
        var isNarrow = width < stateBreakpoint;
        var newState = isNarrow ? NarrowState : WideState;

        if (!isNarrow && DetailsRootContainer.BindingContext != seasonsPageViewModel.CurrentSeason)
            DetailsRootContainer.BindingContext = seasonsPageViewModel.CurrentSeason;

        if (currentState == newState)
            return;

        currentState = newState;

        VisualStateManager.GoToState(this, currentState);

        UpdateDetailsRootContainerCornerRadius(isNarrow ? narrowDetailsRootContainerCornerRadius : wideDetailsRootContainerCornerRadius);

        if (isNarrow)
            isDetailsRootContainerOpen = false;
    }

    private void UpdateDetailsRootContainerCornerRadius(double cornerRadius)
    {
        var roundRect = new RoundRectangle
        {
            CornerRadius = new CornerRadius(cornerRadius, cornerRadius, 0, 0)
        };

        DetailsRootContainer.StrokeShape = roundRect;
    }

    private async Task HideDetails()
    {
        UpdateDetailsRootContainerCornerRadius(narrowDetailsRootContainerCornerRadius);

        uint animationLength = 250;

        DetailsRootContainer.AbortAnimation("TranslationAnimation");

        var animation = new Animation(v =>
        {
            DetailsRootContainer.TranslationY = v;
        }, DetailsRootContainer.TranslationY, detailsHiddenPosition, finished: () =>
        {
            DetailsRootContainer.TranslationY = detailsHiddenPosition;
        });
        animation.Commit(DetailsRootContainer, "TranslationAnimation", length: animationLength);

        await Task.Delay((int)animationLength);

        isDetailsRootContainerOpen = false;
        ShowDetailsButton.IsVisible = !isTouchBased && true;
        HideDetailsButton.IsVisible = false;
        HeaderDetailsLabel.IsVisible = false;
        SwipeUpDetailsContainer.IsVisible = isTouchBased && true;
    }

    private async Task ShowDetails()
    {
        uint animationLength = 250;

        DetailsRootContainer.AbortAnimation("TranslationAnimation");

        var animation = new Animation(v =>
        {
            DetailsRootContainer.TranslationY = v;
        }, DetailsRootContainer.TranslationY, detailsShownPosition, finished: () =>
        {
            DetailsRootContainer.TranslationY = detailsShownPosition;
        });
        animation.Commit(DetailsRootContainer, "TranslationAnimation", length: animationLength);

        await Task.Delay((int)animationLength);

        isDetailsRootContainerOpen = true;
        ShowDetailsButton.IsVisible = !isTouchBased && false;
        HideDetailsButton.IsVisible = true;
        HeaderDetailsLabel.IsVisible = true;
        SwipeUpDetailsContainer.IsVisible = isTouchBased && false;

        UpdateDetailsRootContainerCornerRadius(0);
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
        seasonsPageViewModel.PropertyChanged += SeasonsPageViewModelPropertyChanged;

#if WINDOWS
        image.Behaviors.Add(new CenteredImageBehavior());
#endif
    }

    private void SeasonsPageUnloaded(object sender, EventArgs e)
    {
        seasonsPageViewModel.PropertyChanged -= SeasonsPageViewModelPropertyChanged;
        Loaded -= SeasonsPageLoaded;
        Unloaded -= SeasonsPageUnloaded;
        SizeChanged -= SeasonsPageSizeChanged;
    }

    private void SeasonsPageViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ISeasonsPageViewModel.CurrentSeason))
        {
            if (currentState != NarrowState)
            {
                DetailsRootContainer.BindingContext = seasonsPageViewModel.CurrentSeason;
            }
        }
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
            isDetailsRootContainerOpen = false;
            ShowDetailsButton.IsVisible = !isTouchBased && true;
            HideDetailsButton.IsVisible = false;
            HeaderDetailsLabel.IsVisible = false;
            SwipeUpDetailsContainer.IsVisible = isTouchBased && true;
        }
        else if (currentState == NarrowState)
        {
            detailsHiddenPosition = DetailsRootContainer.Height - detailsHeaderHeight;
        }     
    }

    private async void DetailsPanUpdated(object sender, PanUpdatedEventArgs e)
	{
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                if (DetailsRootContainer.BindingContext != seasonsPageViewModel.CurrentSeason)
                    DetailsRootContainer.BindingContext = seasonsPageViewModel.CurrentSeason;
                break;
            case GestureStatus.Running:
                lastDetailsTranslation = DetailsRootContainer.TranslationY + e.TotalY;
                DetailsRootContainer.TranslationY = Math.Min(Math.Max(lastDetailsTranslation, detailsShownPosition), detailsHiddenPosition);
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
        if (DetailsRootContainer.BindingContext != seasonsPageViewModel.CurrentSeason)
            DetailsRootContainer.BindingContext = seasonsPageViewModel.CurrentSeason;
        await ShowDetails();
    }

    private async void HideButtonClicked(object sender, EventArgs e)
    {
        await HideDetails();
    }
}