using System;
using PicSim.Utils;

namespace PicSim.Components.Registers
{
    /// <summary>
    /// Implements the OPTION_REG register, as detailed in section "4.2.2.2 OPTION_REG REGISTER", p.15.
    /// </summary>
    public class OptionRegister : MemoryCell
    {
        public bool HasFlag(byte bitNo)
        {
            byte mask = (byte)(1 << bitNo);
            return (Value & mask) != 0;
        }

        public void SetFlag(byte bitNo, bool value)
        {
            byte mask = (byte)(1 << bitNo);
            if (value) Value |= mask;
            else Value &= mask.Complement();
        }

        /// <summary>
        /// PORTB Pull-up Enable bit
        /// <para>1 = PORTB pull-ups are disabled</para>
        /// <para>0 = PORTB pull-ups are enabled (by individual port latch values)</para>
        /// </summary>
        public bool RBPU
        {
            get { return HasFlag(7); }
            set { SetFlag(7, value); }
        }

        /// <summary>
        /// Interrupt Edge Select bit
        /// <para>1 = Interrupt on rising edge of RB0/INT pin</para>
        /// <para>0 = Interrupt on falling edge of RB0/INT pin</para>
        /// </summary>
        public bool INTEDG
        {
            get { return HasFlag(6); }
            set { SetFlag(6, value); }
        }

        /// <summary>
        /// TMR0 Clock Source Select bit
        /// <para>1 = Transition on RA4/T0CKI pin</para>
        /// <para>0 = Internal instruction cycle clock (CLKOUT)</para>
        /// </summary>
        public bool TMR0CS
        {
            get { return HasFlag(5); }
            set { SetFlag(5, value); }
        }

        /// <summary>
        /// TMR0 Source Edge Select bit
        /// <para>1 = Increment on high-to-low transition on RA4/T0CKI pin</para>
        /// <para>0 = Increment on low-to-high transition on RA4/T0CKI pin</para>
        /// </summary>
        public bool TMR0SE
        {
            get { return HasFlag(4); }
            set { SetFlag(4, value); }
        }

        /// <summary>
        /// Prescaler Assignment bit
        /// <para>1 = Prescaler assigned to the WDT</para>
        /// <para>0 = Prescaler assigned to TMR0</para>
        /// </summary>
        public bool PSA
        {
            get { return HasFlag(3); }
            set { SetFlag(3, value); }
        }

        /// <summary>
        /// Prescaler Rate Select bits
        /// </summary>
        /// <remarks>
        /// Associated clock rates:
        /// <table>
        /// <thead><tr><td>Bit Value</td><td>TMR0 Rate</td><td>WDT Rate</td></tr></thead>
        /// <tbody>
        /// <tr><td>000</td><td>1 : 2</td><td>1 : 1</td></tr>
        /// <tr><td>001</td><td>1 : 4</td><td>1 : 2</td></tr>
        /// <tr><td>010</td><td>1 : 8</td><td>1 : 4</td></tr>
        /// <tr><td>011</td><td>1 : 16</td><td>1 : 8</td></tr>
        /// <tr><td>100</td><td>1 : 32</td><td>1 : 16</td></tr>
        /// <tr><td>101</td><td>1 : 64</td><td>1 : 32</td></tr>
        /// <tr><td>110</td><td>1 : 128</td><td>1 : 64</td></tr>
        /// <tr><td>111</td><td>1 : 256</td><td>1 : 128</td></tr>
        /// </tbody>
        /// </table>
        /// </remarks>
        public byte Prescaler
        {
            get { return (byte)(Value & (byte)0x7); }
            set
            {
                if (value > 0x7) {
                    throw new ArgumentOutOfRangeException("value", value, "The value must be smaller than or equal to 0x7.");
                }
                Value = (byte)((Value & ((byte)0x7).Complement()) | value);
            }
        }
    }
}
