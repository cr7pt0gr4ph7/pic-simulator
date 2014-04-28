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

        public InterruptManager(Processor processor)
        {
            Ensure.ArgumentNotNull(processor, "processor");

            this.m_processor = processor;
            this.m_contributors = CreateContributors().ToArray();
        }

        private IEnumerable<IInterruptContributor> CreateContributors()
        {
            yield break;
        }

        /// <summary>
        /// Process all events.
        /// NOTE: Must be called AFTER the communication interfaces have been updated etc.
        /// </summary>
        private void PreStep()
        {
            foreach (IInterruptContributor contributor in m_contributors)
            {
                contributor.HandleEvents();
            }
        }

        public bool Step()
        {

            return false;
        }
    }
}
