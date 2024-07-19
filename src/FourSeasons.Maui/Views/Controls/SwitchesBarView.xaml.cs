namespace FourSeasons.Maui.Views.Controls;

public partial class SwitchesBarView : Grid
{
    private double ItemHeight => HeightRequest;
    private double DefaultItemWidth => ItemHeight;
    private double SelectedItemWidth => WidthRequest - (((Items?.Count() ?? 1) - 1) * DefaultItemWidth);

    private readonly SwitchesBarDrawable drawable;
    private object selectedItem = null;

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(nameof(Items), typeof(IEnumerable<object>), typeof(SwitchesBarView), propertyChanged: OnItemsChanged);
	public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(SwitchesBarView), propertyChanged: OnSelectedItemChanged);

    public IEnumerable<object> Items
    {
        get => (IEnumerable<object>) GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }


    public SwitchesBarView()
	{
		InitializeComponent();

		SizeChanged += SwitchesBarViewSizeChanged;

        graphicsView.Drawable = drawable = new SwitchesBarDrawable
        {
            SelectionColor = Color.FromArgb("#FF042351"),
            BackgroundColor = Color.FromArgb("#77FFFFFF")
        };
    }


	private void SwitchesBarViewSizeChanged(object sender, EventArgs e)
	{
        UpdateControl();
    }

    private void UpdateControl()
    {
        if (!absoluteLayout.Children.Any())
            return;

        drawable.Views = absoluteLayout.Children;

        double left = 0;

        for (int i = 0; i < absoluteLayout.Children.Count; i++)
        {
            var itemView = absoluteLayout.Children[i] as View;

            var width = itemView.BindingContext == SelectedItem ? SelectedItemWidth : DefaultItemWidth;

            var rect = new Rect(left, 0, width, ItemHeight);

            if (itemView.BindingContext == SelectedItem)
            {
                drawable.SelectedView = itemView;
                drawable.SelectingOpacity = 1;
                drawable.UnselectingView = null;
                drawable.UnselectingOpacity = 0;

                graphicsView.Invalidate();
            }

            AbsoluteLayout.SetLayoutBounds(itemView, rect); // This works on Windows but does not work on Android
            itemView.Layout(rect); // This works on Android but does not work on Windows

            left += width;
        }
    }

    private void AnimateSelection(object oldSelected, object newSelected)
    {
        var animation = new Animation();

        bool needsToBeChanged = false;
        double left = 0;

        for (int i = 0; i < absoluteLayout.Children.Count; i++)
        {
            var itemView = absoluteLayout.Children[i] as View;

            if (itemView.BindingContext == newSelected || itemView.BindingContext == oldSelected)
            {
                needsToBeChanged = !needsToBeChanged;
                
                var currentLeftSelected = itemView.X;
                var newLeftSelected = left;
                var currentWidthSelected = itemView.Width;

                animation.Add(0, 1, new Animation(v =>
                {
                    var l = currentLeftSelected + ((newLeftSelected - currentLeftSelected) * v);
                    var w = currentWidthSelected + (((itemView.BindingContext == newSelected ? SelectedItemWidth : DefaultItemWidth) - currentWidthSelected) * v);
                    var rect = new Rect(l, 0, w, ItemHeight);

                    if (itemView.BindingContext == newSelected)
                    {
                        drawable.SelectedView = itemView;
                        drawable.SelectingOpacity = v;
                    }
                    else
                    {
                        drawable.UnselectingView = itemView;
                        drawable.UnselectingOpacity = Math.Abs(v - 1);
                    }

                    graphicsView.Invalidate();

                    AbsoluteLayout.SetLayoutBounds(itemView, rect);
                    itemView.Layout(rect);
                }, 0, 1));

                left += itemView.BindingContext == newSelected ? SelectedItemWidth : DefaultItemWidth;
                continue;
            }

            if (!needsToBeChanged)
            {
                left += DefaultItemWidth;
                continue;
            }

            var currentLeft = itemView.X;
            var newLeft = left;
            var currentWidth = itemView.Width;

            animation.Add(0, 1, new Animation(v =>
            {
                var l = currentLeft + ((newLeft - currentLeft) * v);
                var w = currentWidth + ((DefaultItemWidth - currentWidth) * v);
                var rect = new Rect(l, 0, w, ItemHeight);

                AbsoluteLayout.SetLayoutBounds(itemView, rect);
                itemView.Layout(rect);
            }, 0, 1));

            left += DefaultItemWidth;
        }

        animation.Commit(this, "Animation", finished: (v, b) =>
        {
            UpdateControl();
        });
    }

    private void ButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var oldSelectedItem = SelectedItem;

        selectedItem = button.BindingContext;
        SelectedItem = selectedItem;

        if (oldSelectedItem != SelectedItem)
            AnimateSelection(oldSelectedItem, SelectedItem);
    }

    private static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var switchesBarView = bindable as SwitchesBarView;
        var items = newValue as IEnumerable<object>;

        BindableLayout.SetItemsSource(switchesBarView.absoluteLayout, items);

        switchesBarView.UpdateControl();
    }

    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var switchesBarView = bindable as SwitchesBarView;

        if (switchesBarView.selectedItem == newValue)
            return;

        switchesBarView.UpdateControl();
    }

    private class SwitchesBarDrawable : IDrawable
    {
        private float margin = 5;
        private float minOpacity = 0.4f;

        public IView SelectedView { get; set; }
        public IView UnselectingView { get; set; }
        public double SelectingOpacity { get; set; }
        public double UnselectingOpacity { get; set; }
        public Color SelectionColor { get; set; } = Color.FromArgb("#FF042351");
        public Color BackgroundColor { get; set; } = Color.FromArgb("#77FFFFFF");
        public IList<IView> Views { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.SetFillPaint(new SolidColorBrush(BackgroundColor), dirtyRect);
            canvas.FillRoundedRectangle(dirtyRect, dirtyRect.Height / 2f);

            foreach (var view in Views)
            {
                Color color;
                var viewRect = view.Frame;
                RectF rect = new Rect(viewRect.X + margin, viewRect.Y + margin, viewRect.Width - (2 * margin), viewRect.Height - (2 * margin));
                var radius = rect.Height / 2f;

                if (view == SelectedView)
                {
                    var opacity = (SelectingOpacity * (1 - minOpacity)) + minOpacity;
                    color = Color.FromRgba(SelectionColor.Red, SelectionColor.Green, SelectionColor.Blue, opacity);
                }
                else if (view == UnselectingView)
                {
                    var opacity = (UnselectingOpacity * (1 - minOpacity)) + minOpacity;
                    color = Color.FromRgba(SelectionColor.Red, SelectionColor.Green, SelectionColor.Blue, opacity);
                }
                else
                {
                    color = Color.FromRgba(SelectionColor.Red, SelectionColor.Green, SelectionColor.Blue, 0.5f);
                }

                canvas.SetFillPaint(new SolidColorBrush(color), rect);
                canvas.FillRoundedRectangle(rect, radius);
            }

            canvas.RestoreState();
        }
    }
}