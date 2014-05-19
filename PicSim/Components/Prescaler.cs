using PicSim.Execution;

namespace PicSim.Components
{
    /// <summary>
    /// The programmable prescaler.
    //  See "Figure 4-4 OPTION_REG REGISTER (ADDRESS 81H)", p.15.
    /// </summary>
    public class Prescaler
    {
        private readonly Processor m_processor;

        public Prescaler(Processor processor)
        {
            Ensure.ArgumentNotNull(processor, "processor");
            m_processor = processor;
        }

        /// <summary>
        /// Returns whether the prescaler is assigned to <c>TMR0</c>.
        /// </summary>
        public bool IsAssignedToTMR0
        {
            get { return !m_processor.Registers.Option.PSA; }
        }

        /// <summary>
        /// Returns whether the prescaler is assigned to the Watchdog.
        /// </summary>
        public bool IsAssignedToWDT
        {
            get { return m_processor.Registers.Option.PSA; }
        }

        private byte PrescalerBits
        {
            get { return m_processor.Registers.Option.Prescaler; }
        }

        public int TMR0Rate
        {
            get { return 2 << PrescalerBits; }
        }

        public int WDTRate
        {
            get { return 1 << PrescalerBits; }
        }
    }
}
