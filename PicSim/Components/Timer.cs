using PicSim.Components.Notifications;
using PicSim.Components.Registers;
using PicSim.Execution;
using System;
using System.Diagnostics;

namespace PicSim.Components
{
    public class Timer
    {
        private readonly Processor m_processor;
        private readonly TMR0RegisterImpl m_tmr0Register;
        private uint m_internalCounter;
        private bool m_hasOverflowed;

        public Timer(Processor processor)
        {
            Ensure.ArgumentNotNull(processor, "processor");
            this.m_processor = processor;
            this.m_tmr0Register = new TMR0RegisterImpl(this);
        }

        public IRegister TMR0Register
        {
            get { return m_tmr0Register; }
        }

        /// <summary>
        /// Returns <c>true</c> if TMR0 has overflowed in the current instruction cycle.
        /// </summary>
        public bool HasOverflowed
        {
            get { return m_hasOverflowed; }
        }

        bool IsPrescalerAssignedToTMR0
        {
            get { return m_processor.Prescaler.IsAssignedToTMR0; }
        }

        int PrescalerRate
        {
            get { return IsPrescalerAssignedToTMR0 ? m_processor.Prescaler.TMR0Rate : 1; }
        }

        public void PreStep()
        {
            // TODO Implement clock source
            DoIncrement();
        }

        private void DoIncrement()
        {
            // Use "unchecked" arithmetic to allow arithmetic operations (+, -, * and so on)
            // to overflow silently, instead of throwing an OverflowException.
            unchecked
            {
                // Increment our internal "prescaler"
                m_internalCounter = (uint)((m_internalCounter + 1) % PrescalerRate);
                if (m_internalCounter != 0) return;

                m_tmr0Register.RawValue += 1;

                // Check if an overflow happened
                if (m_tmr0Register.RawValue == 0)
                {
                    // Set the overflow state for the TMR0InterruptContributor
                    m_hasOverflowed = true;
                }
            }
        }

        private void ClearPrescaler()
        {
            Debug.Assert(IsPrescalerAssignedToTMR0);
            m_internalCounter = 0;
        }

        public void PostStep()
        {
            // Reset the overflow state
            m_hasOverflowed = false;
        }

        private class TMR0RegisterImpl : IRegister
        {
            private readonly Timer m_outer;
            private byte m_value;

            public TMR0RegisterImpl(Timer outer)
            {
                Ensure.ArgumentNotNull(outer, "outer");
                m_outer = outer;
            }

            public byte Value
            {
                get { return m_value; }
                set { SetValue(value, true); }
            }

            public byte RawValue
            {
                get { return m_value; }
                set { SetValue(value, false); }
            }

            private void SetValue(byte newValue, bool isExternal)
            {
                if (m_value != newValue)
                {
                    m_value = newValue;
                    OnRegisterChanged(m_value);

                    // External set of the TMR0 register?
                    // If yes: Reset the prescaler (but only if it is assigned to TMR0
                    if (isExternal && m_outer.IsPrescalerAssignedToTMR0) m_outer.ClearPrescaler();
                }
            }

            private void OnRegisterChanged(byte newValue)
            {
                var handler = RegisterChanged;
                if (handler != null) handler(this, new RegisterChangedEventArgs(newValue));
            }

            public event EventHandler<RegisterChangedEventArgs> RegisterChanged;
        }
    }
}
