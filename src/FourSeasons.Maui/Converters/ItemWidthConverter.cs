using System.Globalization;

namespace FourSeasons.Maui.Converters;

public class ItemWidthConverter : IValueConverter
{
    public double Spacing { get; set; } = 0;
    public double CountInRow { get; set; } = 1;
    public double MaximumWidth { get; set; } = 1;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var totalWidth = (double)value;
        var width = Math.Min(MaximumWidth, (totalWidth - ((CountInRow - 1) * Spacing)) / CountInRow);

        return width;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}