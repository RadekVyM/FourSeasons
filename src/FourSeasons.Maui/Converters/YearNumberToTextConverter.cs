using System.Globalization;

namespace FourSeasons.Maui.Converters;

public class YearNumberToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var year = (int)value;
        var yearNow = DateTime.Today.Year;

        if (year == yearNow)
            return "This Year";
        else if (year == yearNow - 1)
            return "Last Year";

        return year.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}