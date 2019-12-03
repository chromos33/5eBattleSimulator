using Battle_Simulator.CharacterStuff;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Battle_Simulator.Converter
{
    public class DiceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Dice)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }
    }
}