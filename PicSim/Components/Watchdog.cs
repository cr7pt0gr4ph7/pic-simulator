﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components
{
    public class Watchdog
    {
        private readonly IResetListener m_resetListener;

        private bool enable = false;
        private uint postScaler = 1;
        private bool timeOut = false;

        public Watchdog(IResetListener resetListener)
        {
            Ensure.ArgumentNotNull(resetListener, "resetListener");
            m_resetListener = resetListener;
        }

        /// <summary>
        /// Clear the watchdog timer, to prevent it from triggering.
        /// </summary>
        public void Clear()
        {
            // TODO
        }

        public bool IsTriggered { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns>
        /// <c>true</c> to cancel processing the current instruction cycle (because the watchdog triggered);
        /// <c>false</c> to resume the current instruction cycle as normal.
        /// </returns>
        public bool Step()
        {
            if (IsTriggered) {
                // Generate a reset condition
                m_resetListener.WatchdogReset();

                // Skip the rest of the instruction cycle
                // (See Processor.Step() for more information)
                return true;
            } else {
                return false;
            }
        }

        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        public bool TimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }

        public uint PostScaler
        {
            get { return postScaler; }
            set { postScaler = value;}
            // TODO: Gültigkeitsüberprüfung
        }
    }
}
