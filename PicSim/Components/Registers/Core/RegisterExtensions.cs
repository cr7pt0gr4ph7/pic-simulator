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
    }
}
