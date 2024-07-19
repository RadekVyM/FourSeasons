using Microsoft.Maui.Controls.Shapes;
using System.Globalization;

namespace FourSeasons.Maui.Converters;

public class TrendToGeometryConverter : IValueConverter
{
    private readonly PathGeometryConverter pathGeometryConverter = new PathGeometryConverter();
    private Geometry growthGeometry;
    private Geometry descentGeometry;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var trend = (int)value;

        if (trend < 0)
            return descentGeometry ??= pathGeometryConverter.ConvertFromInvariantString("M0,0 L8,9 L16,0") as Geometry;
        else
            return growthGeometry ??= pathGeometryConverter.ConvertFromInvariantString("M0,9 L8,0 L16,9") as Geometry;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}