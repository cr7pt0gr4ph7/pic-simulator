using PicSim.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components
{
    public class InterruptManager
    {
        private readonly Processor m_processor;

        public InterruptManager(Processor processor)
        {
            Ensure.ArgumentNotNull(processor, "processor");
            this.m_processor = processor;
        }

        public bool Step()
        {
            return false;
        }
    }
}
