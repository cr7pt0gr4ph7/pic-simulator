namespace PicSim.Components
{
    public class VirtualClock : ModelBase
    {
        private long m_totalCycles;

        /// <summary>
        /// Reset the instruction cycle counter to zero.
        /// </summary>
        public void Reset()
        {
            m_totalCycles = 0;
            RaisePropertyChanged("TotalCycles");
        }

        /// <summary>
        /// Advance the instruction cycle counter by <paramref name="machineCycles"/> instruction cycles.
        /// </summary>
        /// <param name="machineCycles"></param>
        public void AdvanceBy(int machineCycles)
        {
            m_totalCycles += machineCycles;
            RaisePropertyChanged("TotalCycles");
        }

        /// <summary>
        /// Returns the number of total instruction cycles since the start of the simulation.
        /// </summary>
        public long TotalCycles
        {
            get { return m_totalCycles; }
        }
    }
}
