using PicSim.Components.Notifications;
using PicSim.Components.Registers;
using PicSim.Utils;
using System;

namespace PicSim.Components
{
    /// <summary>
    /// Models the program counter of the processor.
    /// See "Section 4.3 Program Counter: PCL and PCLATH", p.17.
    /// </summary>
    public class ProgramCounter
    {
        private const ushort BIT_MASK_13_BITS = 0x1FFF;
        private const ushort BIT_MASK_11_BITS = 0x7FF;

        private readonly PclRegister m_pclRegister;
        private readonly PclathRegister m_pclathRegister;

        private bool m_changedInCurrentStep;
        private ushort m_programCounter;
        private byte m_pclathValue;

        public ProgramCounter()
        {
            m_pclRegister = new PclRegister(this);
            m_pclathRegister = new PclathRegister(this);
        }

        /// <summary>
        /// Whether the program counter has been changed externally
        /// during the current step.
        /// </summary>
        public bool ChangedInCurrentStep
        {
            get { return m_changedInCurrentStep; }
        }

        public event EventHandler<ValueChangedEventArgs<ushort>> ValueChanged;

        /// <summary>
        /// The address of the opcode that is currently being executed.
        /// The  Program  Counter  (PC)  is  13-bits  wide.
        /// </summary>
        public ushort Value
        {
            get { return m_programCounter; }
            /*private*/
            set
            {
                // Simulate the behavior of the PIC when the PC is changed manually
                // TODO This also takes effect if the PC is changed via the debugger (but maybe it should not)!
                m_changedInCurrentStep = true;
                m_programCounter = value;

                m_pclRegister.OnRegisterChanged();
                ValueChanged.RaiseIfNotNull(this, new ValueChangedEventArgs<ushort>(m_programCounter));
            }
        }

        /// <summary>
        /// The value (PC + 1), w.r.t. to the correct overflow behavior
        /// related to the program counter only being 13 bits wide.
        /// </summary>
        public ushort NextValue
        {
            get
            {
                // TODO This does not take into account that the PC is supposed to be 13 bits wide
                //      when it comes to overflowing at 2^13.
                return (ushort)(Value + 1);
            }

        }

        public IRegister PCL_Register
        {
            get { return m_pclRegister; }
        }

        public IRegister PCLATH_Register
        {
            get { return m_pclathRegister; }
        }

        #region Pre-step / Post-step

        /// <summary>
        /// Prepare the program counter for the next step.
        /// Called by <see cref="PicSim.Execution.Processor.PreStep"/>.
        /// </summary>
        public void PreStep()
        {
            // Reset the "changed in current step" flag
            m_changedInCurrentStep = false;
        }

        public void PostStep()
        {
            // Increment the PC if it has not already been changed
            if (!ChangedInCurrentStep)
            {
                Value = NextValue;
            }
        }

        #endregion

        #region Different load methods

        /// <summary>
        /// Load the PC with an 8-bit value.
        /// See "Figure 4-6 Loading of PC in different situations", p.17.
        /// </summary>
        /// <remarks>
        /// The lower bits of PC are loaded with <paramref name="lowerBits"/>.
        /// The higher bits of PC are loaded with <c><![CDATA[PCLATH<4:0>]]></c>.
        /// </remarks>
        /// <param name="lowerBits">The lower 8-bit of the new value of the PC.</param>
        public void LoadFrom8Bits(byte lowerBits)
        {
            this.Value = (ushort)(((m_pclathValue & PclathRegister.ALL_BITS_MASK) << 8) | lowerBits);
        }

        /// <summary>
        /// Load the PC with an 11-bit value.
        /// See "Figure 4-6 Loading of PC in different situations", p.17.
        /// </summary>
        /// <remarks>
        /// The lower 11 bits of PC are loaded with <paramref name="lowerBits"/>.
        /// The highest 2 bits of PC are loaded with <c><![CDATA[PCLATH<4:0>]]></c>.
        /// </remarks>
        /// <param name="lowerBits">The lower 11 bits of the new value of the PC.</param>
        public void LoadFrom11Bits(ushort lowerBits)
        {
            this.Value = (ushort)(((m_pclathValue & PclathRegister.HIGH_BITS_MASK) << 8) | (lowerBits & BIT_MASK_11_BITS));
        }

        /// <summary>
        /// Load the PC with an 13-bit value.
        /// See "Section 4.4 Stack", p.17.
        /// </summary>
        /// <remarks>
        /// The lower 13 bits of PC are loaded with <paramref name="lowerBits"/>.
        /// </remarks>
        /// <param name="lowerBits">The lower 11 bits of the new value of the PC.</param>
        public void LoadFrom13Bits(ushort newValue)
        {
            this.Value = (ushort)(newValue & BIT_MASK_13_BITS);
        }

        #endregion

        #region PCL and PCLATH registers

        /// <summary>
        /// The PCL register. Loading this register changes the program counter.
        /// </summary>
        private class PclRegister : IRegister
        {
            private readonly ProgramCounter m_outer;

            public PclRegister(ProgramCounter outer)
            {
                Ensure.ArgumentNotNull(outer, "outer");
                m_outer = outer;
            }

            /// <summary>
            /// Returns the lower 8-bits of the program counter, or loads the lower 8 bits
            /// of the program counter with the assigned value.
            /// </summary>
            public byte Value
            {
                get { return (byte)(m_outer.Value & 0xFF); }
                set { m_outer.LoadFrom8Bits(value); }
            }

            internal void OnRegisterChanged()
            {
                var handler = RegisterChanged;
                if (handler != null)
                {
                    handler(this, new Notifications.RegisterChangedEventArgs(Value));
                }
            }

            public event System.EventHandler<Notifications.RegisterChangedEventArgs> RegisterChanged;
        }

        private class PclathRegister : IRegister
        {
            public const byte ALL_BITS_MASK = 0x1F;
            public const byte HIGH_BITS_OFFSET = 3;
            public const byte HIGH_BITS_MASK = 0x24;

            private readonly ProgramCounter m_outer;

            public PclathRegister(ProgramCounter outer)
            {
                Ensure.ArgumentNotNull(outer, "outer");
                m_outer = outer;
            }

            public byte Value
            {
                get { return m_outer.m_pclathValue; }
                set
                {
                    var newValue = (byte)((m_outer.m_pclathValue & ALL_BITS_MASK.Complement()) | (value & ALL_BITS_MASK));
                    if (newValue != m_outer.m_pclathValue)
                    {
                        m_outer.m_pclathValue = newValue;
                        OnRegisterChanged(newValue);
                    }
                }
            }

            private void OnRegisterChanged(byte newValue)
            {
                var handler = RegisterChanged;
                if (handler != null)
                {
                    handler(this, new Notifications.RegisterChangedEventArgs(newValue));
                }
            }

            public event System.EventHandler<Notifications.RegisterChangedEventArgs> RegisterChanged;
        }

        #endregion
    }
}
