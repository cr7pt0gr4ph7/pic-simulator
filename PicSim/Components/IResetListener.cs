using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components
{
    public interface IResetListener
    {
        /// <summary>
        /// Called on a watchdog reset.
        /// </summary>
        void WatchdogReset();
    }
}
