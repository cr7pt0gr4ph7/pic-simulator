using PicSim.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PicSim.UI.Converters
{
    [ValueConversion(typeof(IOPortDirection), typeof(string))]
    public class PortDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is IOPortDirection)) return DependencyProperty.UnsetValue;
            switch ((IOPortDirection)value)
            {
                case IOPortDirection.Input: return "i";
                case IOPortDirection.Output: return "o";
                default: throw new InvalidOperationException("Invalid IOPortDirection value: " + value.ToString());
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is string)) return DependencyProperty.UnsetValue;
            switch (((string)value).ToLower())
            {
                case "i": return IOPortDirection.Input;
                case "o": return IOPortDirection.Output;
                default: return DependencyProperty.UnsetValue;
            }
        }
    }
}
