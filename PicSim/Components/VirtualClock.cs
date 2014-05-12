using System;
namespace PicSim.Components
{
    public class VirtualClock : ModelBase
    {
        private TimeSpan m_simulatedTime;
        private int m_frequency;
        private long m_totalCycles;

        public VirtualClock()
        {
            m_frequency = 4000000 /* Hz = 4 MHz */;
            Reset();
        }

        /// <summary>
        /// Reset the instruction cycle counter to zero.
        /// </summary>
        public void Reset()
        {
            TotalCycles = 0;
            SimulatedTime = TimeSpan.Zero;
        }

        /// <summary>
        /// Advance the instruction cycle counter by <paramref name="machineCycles"/> instruction cycles.
        /// </summary>
        /// <param name="machineCycles"></param>
        public void AdvanceBy(int machineCycles)
        {
            TotalCycles += machineCycles;
            SimulatedTime += TimeSpan.FromTicks(machineCycles * (TimeSpan.TicksPerSecond / m_frequency));
        }

        /// <summary>
        /// Returns the number of total instruction cycles since the start of the simulation.
        /// </summary>
        public long TotalCycles
        {
            get { return m_totalCycles; }
            private set { SetProperty(ref m_totalCycles, value); }
        }

        /// <summary>
        /// The clock frequency of the simulated processor.
        /// </summary>
        public int Frequency
        {
            get { return m_frequency; }
            set { Ensure.ArgumentNotNegative(value, "value"); SetProperty(ref m_frequency, value); }
        }

        /// <summary>
        /// The amount of simulated time that has elapsed since the start of the simulation.
        /// </summary>
        public TimeSpan SimulatedTime
        {
            get { return m_simulatedTime; }
            private set { SetProperty(ref m_simulatedTime, value); }
        }
    }
}
