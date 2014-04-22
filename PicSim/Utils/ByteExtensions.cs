using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Utils
{
    public static class ByteExtensions
    {
        /// <summary>
        /// Returns the bitwise complement of the specified byte.
        /// </summary>
        /// <param name="b">The byte whose complement should be returned.</param>
        /// <returns>The bitwise complement of <paramref name="b"/>.</returns>
        public static byte Complement(this byte b)
        {
            return (byte)(b ^ 0xFF);
        }

        private static byte GetMaskForBit(byte bitNo)
        {
            return (byte)(1 << bitNo);
        }

        public static bool GetBit(this byte value, byte bitNo)
        {
            return (value & GetMaskForBit(bitNo)) != 0;
        }

        public static byte SetBit(this byte value, byte bitNo)
        {
            return (byte)(value | GetMaskForBit(bitNo));
        }

        public static byte ClearBit(this byte value, byte bitNo)
        {
            return (byte)(value & GetMaskForBit(bitNo).Complement());
        }
    }
}
