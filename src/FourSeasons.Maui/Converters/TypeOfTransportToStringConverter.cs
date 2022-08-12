using FourSeasons.Core.ViewModels;
using System.Globalization;

namespace FourSeasons.Maui.Converters
{
    public class TypeOfTransportToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var typeOfTransport = (TypeOfTransport)value;

            return typeOfTransport switch
            {
                TypeOfTransport.Car => "Car",
                _ => "Car"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
