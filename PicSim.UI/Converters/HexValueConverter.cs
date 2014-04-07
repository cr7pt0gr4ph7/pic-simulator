using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PicSim.UI.Converters
{
    public class HexValueConverter : IValueConverter
    {
        private static readonly Dictionary<Type, Func<String, object>> m_type2parseMethod;

        static HexValueConverter()
        {
            m_type2parseMethod = new Dictionary<Type, Func<string, object>>() { 
                { typeof(SByte), s => SByte.Parse(s, NumberStyles.HexNumber) },                
                { typeof(Int16), s => Int16.Parse(s, NumberStyles.HexNumber) },
                { typeof(Int32), s => Int32.Parse(s, NumberStyles.HexNumber) },
                { typeof(Int64), s => Int64.Parse(s, NumberStyles.HexNumber) },
                
                { typeof(Byte), s => Byte.Parse(s, NumberStyles.HexNumber) },
                { typeof(UInt16), s => UInt16.Parse(s, NumberStyles.HexNumber) },
                { typeof(UInt32), s => UInt16.Parse(s, NumberStyles.HexNumber) },
                { typeof(UInt64), s => UInt16.Parse(s, NumberStyles.HexNumber) },
            };
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // TODO Don't create a format string on every single conversion
            return ((IFormattable)value).ToString("X" + Digits.ToString(), null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try {
                return m_type2parseMethod[targetType]((string)value);
            }
            catch (FormatException e) {
                // TODO Don't use exceptions for invalid format detection
                return DependencyProperty.UnsetValue;
            }
        }

        public int Digits { get; set; }
    }
}
