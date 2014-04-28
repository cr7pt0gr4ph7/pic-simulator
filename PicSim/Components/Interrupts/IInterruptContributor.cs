using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Interrupts
{
    public interface IInterruptContributor
    {
        /// <summary>
        /// Handle the external events that have occured since the last call by setting the appropriate
        /// interrupt flags.
        /// </summary>
        bool HandleEvents();
    }
}
