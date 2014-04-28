using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PicSim.UI.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class Bit2BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value) return "1";
            else return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((value as string) == "1") return true;
            else if ((value as string) == "0") return false;
            else return Binding.DoNothing;
        }
    }
}
