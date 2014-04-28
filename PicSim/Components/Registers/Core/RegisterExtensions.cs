using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Utils;

namespace PicSim.Components.Registers
{
    public static class RegisterExtensions
    {
        #region Bit manipulation

        private static byte GetMaskForBit(byte bitNo)
        {
            return (byte)(1 << bitNo);
        }

        public static bool GetBit(this IRegister register, byte bitNo)
        {
            return (register.Value & GetMaskForBit(bitNo)) != 0;
        }

        public static void SetBit(this IRegister register, byte bitNo)
        {
            register.Value |= GetMaskForBit(bitNo);
        }

        public static void ClearBit(this IRegister register, byte bitNo)
        {
            register.Value &= GetMaskForBit(bitNo).Complement();
        }

        #endregion

        #region Upper/lower nibble manipulation

        public static byte GetLowerNibble(this IRegister register)
        {
            return register.Value.GetLowerNibble();
        }

        public static byte GetUpperNibble(this IRegister register)
        {
            return register.Value.GetUpperNibble();
        }

        public static void SetLowerNibble(this IRegister register, byte newLowerNibble)
        {
            register.Value = register.Value.SetUpperNibble(newLowerNibble);
        }

        public static void SetUpperNibble(this IRegister register, byte newUpperNibble)
        {
            register.Value = register.Value.SetUpperNibble(newUpperNibble);
        }

        #endregion
    }
}
