namespace FourSeasons.Maui.Views.Controls;

public partial class AttractionsView : ContentView
{
	public AttractionsView()
	{
		InitializeComponent();

		SizeChanged += AttractionsViewSizeChanged;
		Unloaded += AttractionsViewUnloaded;
		rootGrid.ChildAdded += RootGridChildAdded;
		rootGrid.ChildRemoved += RootGridChildRemoved;
	}

	private void AttractionsViewSizeChanged(object sender, EventArgs e)
	{
		// This is workaround of the MaximumWidthRequest bug
#if WINDOWS
		var maximumWidth = 400;
		var totalWidth = Math.Min(maximumWidth, this.Width);
        var gridLength = new GridLength((totalWidth - (2 * rootGrid.ColumnSpacing)) / 3);

        rootGrid.WidthRequest = totalWidth;
		rootGrid.ColumnDefinitions = new ColumnDefinitionCollection(
			new ColumnDefinition(gridLength),
            new ColumnDefinition(gridLength),
            new ColumnDefinition(gridLength));
#endif
	}

	private void AttractionsViewUnloaded(object sender, EventArgs e)
    {
        SizeChanged -= AttractionsViewSizeChanged;
        Unloaded -= AttractionsViewUnloaded;
        rootGrid.ChildAdded -= RootGridChildAdded;
        rootGrid.ChildRemoved -= RootGridChildRemoved;
    }

    private void RootGridChildRemoved(object sender, ElementEventArgs e)
    {
        UpdateGrid();
    }

	private void RootGridChildAdded(object sender, ElementEventArgs e)
	{
		UpdateGrid();
	}

	private void UpdateGrid()
	{
		for (int i = 0; i < rootGrid.Count; i++)
		{
			var view = rootGrid[i] as View;

			Grid.SetColumn(view, i);
		}
	}
}