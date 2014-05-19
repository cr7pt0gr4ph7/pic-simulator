using PicSim.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Utils;

namespace PicSim.Components.Interrupts.Contributors
{
    /// <summary>
    /// This interrupt contributor contributes the <c>TMR0</c> interrupt.
    /// </summary>
    public class TMR0InterruptContributor : IInterruptContributor
    {
        private readonly Processor m_processor;

        public TMR0InterruptContributor(Processor processor)
        {
            Ensure.ArgumentNotNull(processor, "processor");
            m_processor = processor;
        }

        public bool HandleEvents()
        {
            bool interruptFlag = m_processor.Timer.HasOverflowed;
            if (interruptFlag) m_processor.Registers.INTCON.T0IF = true;
            return interruptFlag && m_processor.Registers.INTCON.T0IE;
        }
    }
}
