using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim
{
    public static class Ensure
    {
        public static void ArgumentNotNull(object value, string argumentName)
        {
            if (value == null) {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ArgumentNotNullOrEmpty(string value, string argumentName)
        {
            if (value == null) {
                throw new ArgumentNullException(argumentName, "The value must not be null or empty, but was null.");
            } else if (value == "") {
                throw new ArgumentException("The value must not be null or empty, but was an empty string.", argumentName);
            }
        }


        public static void ArgumentNotNegative(int value, string argumentName)
        {
            if (value < 0) {
                throw new ArgumentOutOfRangeException(argumentName, value, "The value must not be negative.");
            }
        }
    }
}
