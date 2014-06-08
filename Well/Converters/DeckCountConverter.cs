using System;
using System.Globalization;
using System.Windows.Data;
using Well.Objects;

namespace Well.Converters
{
    [ValueConversion(typeof (object), typeof (object))]
    public class DeckCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = (DeckCollection) value;
            Deck deck = collection[parameter.ToString()];
            return deck.Count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}