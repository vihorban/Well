using System;
using System.Globalization;
using System.Windows.Data;
using Well.Objects;
using Well.Properties;

namespace Well.Converters
{
    [ValueConversion(typeof(object), typeof(object))]
    public class ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = (DeckCollection) value;
            Deck deck = collection[parameter.ToString()];
            string folder = "cards" + Settings.Default.CardStyleSelectedNumber + "\\";
            return deck.DisplayCard().Path(folder);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}