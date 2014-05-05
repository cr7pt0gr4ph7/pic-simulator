using PicSim.Execution;
using PicSim.Utils;

namespace PicSim.Components.Interrupts.Contributors
{
    public class PortBInterruptContributor : IInterruptContributor
    {
        private readonly Processor m_processor;
        private byte m_lastValue;

        public PortBInterruptContributor(Processor processor)
        {
            Ensure.ArgumentNotNull(processor, "processor");

            m_processor = processor;
            m_lastValue = processor.Registers.PORTB.Value;
        }

        public bool HandleEvents()
        {
            var newValue = m_processor.Registers.PORTB.Value;
            var interruptFlag = (GetRelevantBitsOf(newValue) != GetRelevantBitsOf(m_lastValue));
            m_lastValue = newValue;

            if (interruptFlag) m_processor.Registers.INTCON.RBIF = true;
            return interruptFlag && m_processor.Registers.INTCON.RBIE;
        }

        private byte GetRelevantBitsOf(byte unmaskedValue)
        {
            return unmaskedValue.GetMaskedValue(m_processor.Registers.TRISB.Value).GetUpperNibble();
        }
    }
}
