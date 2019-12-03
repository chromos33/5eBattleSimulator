using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Battle_Simulator.Converter
{
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Int32.TryParse(RemoveNANCharacters((string)value), out int result);
            return result;
        }

        private string RemoveNANCharacters(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }
    }
}
