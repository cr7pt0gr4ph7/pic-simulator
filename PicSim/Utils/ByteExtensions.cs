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

        public static byte GetMaskedValue(this byte value, byte mask)
        {
            return (byte)(value & mask);
        }

        #region Bit manipulation

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

        #endregion

        #region Upper/lower nibble manipulation

        public static byte GetLowerNibble(this byte value)
        {
            return (byte)((value & 0xF) | 0x30);
        }

        public static byte GetUpperNibble(this byte value)
        {
            return (byte)(((value & 0xF0) >> 4) | 0x30);
        }

        public static byte SetLowerNibble(this byte value, byte newLowerNibble)
        {
            return (byte)((newLowerNibble & 0x0F) | (value & 0xF0));
        }

        public static byte SetUpperNibble(this byte value, byte newUpperNibble)
        {
            return (byte)((value & 0x0F) << 4 | (newUpperNibble & 0x0F));
        }

        #endregion
    }
}
