using PicSim.Components.Interrupts.Contributors;
using PicSim.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Interrupts
{
    public class InterruptManager
    {
        private readonly Processor m_processor;
        private readonly IEnumerable<IInterruptContributor> m_contributors;
        private bool m_hasInterrupts;

        public InterruptManager(Processor processor)
        {
            Ensure.ArgumentNotNull(processor, "processor");

            this.m_processor = processor;
            this.m_contributors = CreateContributors().ToArray();
        }

        private IEnumerable<IInterruptContributor> CreateContributors()
        {
            yield return new PortBInterruptContributor(m_processor);
            yield return new RB0INTInterruptContributor(m_processor);
            yield return new TMR0InterruptContributor(m_processor);
        }

        /// <summary>
        /// Process all events.
        /// NOTE: Must be called AFTER the communication interfaces have been updated etc.
        /// </summary>
        public void PreStep()
        {
            m_hasInterrupts = false;
            foreach (IInterruptContributor contributor in m_contributors)
            {
                m_hasInterrupts |= contributor.HandleEvents();
            }
        }

        public bool Step()
        {
            if (m_hasInterrupts)
            {
                // Clear the GIE Bit
                m_processor.Registers.INTCON.GIE = false;

                // Load the ISR vector
                m_processor.Stack.Push(m_processor.ProgramCounter.NextValue);
                m_processor.ProgramCounter.LoadFrom13Bits(0x04);
            }

            // TODO Handle interrupts
            return m_hasInterrupts;
        }
    }
}
