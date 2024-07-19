using FourSeasons.Core.ViewModels;

namespace FourSeasons.Maui.Views.Controls;

public class MonthlyStatsChartView : GraphicsView
{
    private readonly MonthlyStatsChartDrawable chartDrawable;
    private readonly Color lastBarGrowthColor = Color.FromRgba("#82cd70ff");
    private readonly Color lastBarDescentColor = Color.FromRgba("#d73435ff");

    private TouristStatsViewModel viewModel => BindingContext as TouristStatsViewModel;


    public MonthlyStatsChartView()
    {
        Drawable = chartDrawable = new MonthlyStatsChartDrawable
        {
            DefaultBarColor = Color.FromRgba("#eceff4ff")
        };
    }


    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (viewModel is null)
            return;

        chartDrawable.Stats = viewModel.MonthlyTouristStats.OrderBy(m => m.Month).Select(m => m.Stats).ToList();
        chartDrawable.LastBarColor = viewModel.Trend < 0 ? lastBarDescentColor : lastBarGrowthColor;

        Invalidate();
    }

    private class MonthlyStatsChartDrawable : IDrawable
    {
        private readonly float barMargin = 1;
        private readonly float barCornerRadius = 2;

        public IList<int> Stats { get; set; }
        public Color LastBarColor { get; set; }
        public Color DefaultBarColor { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            var barWidth = dirtyRect.Width / 12;
            var maxValue = Stats.Max();
            var defaultBrush = new SolidColorBrush(DefaultBarColor);
            var lastBrush = new SolidColorBrush(LastBarColor);
            float left = 0;

            for (int i = 0; i < Stats.Count; i++)
            {
                var value = Stats[i];

                var barHeight = dirtyRect.Height * ((float)value / maxValue);
                var rect = new RectF(left + barMargin, dirtyRect.Height - barHeight, barWidth - (2 * barMargin), barHeight);

                canvas.SetFillPaint(i == Stats.Count - 1 ? lastBrush : defaultBrush, rect);
                canvas.FillRoundedRectangle(rect, barCornerRadius);

                left += barWidth;
            }
        }
    }
}