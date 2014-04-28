using PicSim.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PicSim.UI.Converters
{
    [ValueConversion(typeof(IOPortDirection), typeof(string))]
    public class PortDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((IOPortDirection)value)
            {
                case IOPortDirection.Input: return "i";
                case IOPortDirection.Output: return "o";
                default: throw new InvalidOperationException("Invalid IOPortDirection value: " + value.ToString());
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((string)value)
            {
                case "i": return IOPortDirection.Input;
                case "o": return IOPortDirection.Output;
                default: return Binding.DoNothing;
            }
        }
    }
}
