using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PicSim.UI.Converters
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class FixedStringConverter : IValueConverter
    {
        public string TrueString { get; set; }
        public string FalseString { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value) return TrueString;
                else return FalseString;
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                if (((string)value) == TrueString)
                {
                    return true;
                }
                else if (((string)value) == FalseString)
                {
                    return false;
                }
            }

            return DependencyProperty.UnsetValue;
        }
    }
}