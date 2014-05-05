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
    /// This interrupt contributor handles <c>RB0/INT</c> interrupts.
    /// </summary>
    public class RB0INTInterruptContributor : IInterruptContributor
    {
        private readonly Processor m_processor;
        private bool m_lastValue;

        public RB0INTInterruptContributor(Processor processor)
        {
            Ensure.ArgumentNotNull(processor, "processor");

            m_processor = processor;
            m_lastValue = GetRelevantBitOf(m_processor.Registers.PORTB.Value);
        }

        public bool HandleEvents()
        {
            var newValue = GetRelevantBitOf(m_processor.Registers.PORTB.Value);
            bool interruptFlag = false;
            if (m_lastValue != newValue)
            {
                if (m_processor.Registers.Option.INTEDG) interruptFlag = newValue;
                else interruptFlag = !newValue;
            }
            m_lastValue = newValue;

            if (interruptFlag) m_processor.Registers.INTCON.INTF = true;
            return interruptFlag && m_processor.Registers.INTCON.INTE;
        }

        private bool GetRelevantBitOf(byte unmaskedValue)
        {
            return unmaskedValue.GetMaskedValue(m_processor.Registers.TRISB.Value.GetMaskedValue(0x01)).GetUpperNibble() != 0;
        }
    }
}
