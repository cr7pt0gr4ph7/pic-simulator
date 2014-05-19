using PicSim.Components.Registers;
using PicSim.Execution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components
{
    public class Timer
    {
        private readonly Processor m_processor;
        private readonly MemoryCell m_tmr0Register = new MemoryCell();
        private bool m_hasOverflowed;

        public Timer(Processor processor)
        {
            Ensure.ArgumentNotNull(processor, "processor");
            this.m_processor = processor;
        }

        public IRegister TMR0Register
        {
            get { return m_tmr0Register; }
        }

        public bool HasOverflowed
        {
            get { return m_hasOverflowed; }
        }

        public void PreStep()
        {
            // TODO Implement the prescaler
            // TODO Implement clock source
            DoIncrement();
        }

        private void DoIncrement()
        {
            // Use "unchecked" arithmetic to allow arithmetic operations (+, -, * and so on)
            // to overflow silently, instead of throwing an OverflowException.
            unchecked
            {
                m_tmr0Register.Value += 1;

                // Check if an overflow happened
                if (m_tmr0Register.Value == 0)
                {
                    // Set the overflow state for the TMR0InterruptContributor
                    m_hasOverflowed = true;
                }
            }
        }

        public void PostStep()
        {
            // Reset the overflow state
            m_hasOverflowed = false;
        }
    }
}
