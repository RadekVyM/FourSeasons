using System.Globalization;

namespace FourSeasons.Maui.Converters
{
    public class TrendColorConverter : IValueConverter
    {
        private readonly Color growthColor = Color.FromRgba("#82cd70ff");
        private readonly Color descentColor = Color.FromRgba("#d73435ff");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var trend = (int)value;

            return trend < 0 ? descentColor : growthColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
