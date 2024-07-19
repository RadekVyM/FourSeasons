using System.Globalization;

namespace FourSeasons.Maui.Converters;

public class BoolConverter : IValueConverter
{
    public object True { get; set; }
    public object False { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return bool.Parse(value.ToString()) ? True : False;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}