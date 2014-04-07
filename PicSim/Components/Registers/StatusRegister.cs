using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Utils;

namespace PicSim.Components.Registers
{
    /// <summary>
    /// Implements the STATUS register, as detailed in section "4.2.2.1 STATUS REGISTER", p.14.
    /// </summary>
    public class StatusRegister : MaskedRegister
    {
        [Flags]
        public enum Flags : byte
        {
            Carry = 1,
            DigitCarry = Carry << 1,
            Zero = DigitCarry << 1,
            NotPowerDown = Zero << 1,
            NotTimeOut = NotPowerDown << 1,
            RP0 = NotTimeOut << 1,
            RP1 = RP0 << 1,
            IRP = RP1 << 1
        }

        public StatusRegister()
            : base(0xE7)
        { }

        #region Flag-based access

        private Flags FlagsValue
        {
            get { return (Flags)RawValue; }
            set { RawValue = (byte)value; }
        }

        public bool HasFlag(Flags bit)
        {
            return FlagsValue.HasFlag(bit);
        }

        public void SetFlag(Flags bit, bool value)
        {
            if (value) FlagsValue |= bit;
            else FlagsValue &= ~bit;
        }

        #endregion

        #region Name-based access

        public bool RP0
        {
            get { return HasFlag(Flags.RP0); }
            set { SetFlag(Flags.RP0, value); }
        }

        public bool NotTimeOut
        {
            get { return HasFlag(Flags.NotTimeOut); }
            set { SetFlag(Flags.NotTimeOut, value); }
        }

        public bool NotPowerDown
        {
            get { return HasFlag(Flags.NotPowerDown); }
            set { SetFlag(Flags.NotPowerDown, value); }
        }

        public bool Zero
        {
            get { return HasFlag(Flags.Zero); }
            set { SetFlag(Flags.Zero, value); }
        }

        public bool DigitCarry
        {
            get { return HasFlag(Flags.DigitCarry); }
            set { SetFlag(Flags.DigitCarry, value); }
        }

        public bool Carry
        {
            get { return HasFlag(Flags.Carry); }
            set { SetFlag(Flags.Carry, value); }
        }

        #endregion
    }
}
