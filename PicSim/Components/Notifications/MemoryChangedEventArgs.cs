using System;

namespace PicSim.Components.Notifications
{
    public class MemoryChangedEventArgs : EventArgs
    {
        private readonly ushort m_address;
        private readonly byte m_newValue;

        public MemoryChangedEventArgs(ushort address, byte newValue)
        {
            m_address = address;
            m_newValue = newValue;
        }

        public ushort Address
        {
            get { return m_address; }
        }

        public byte NewValue
        {
            get { return m_newValue; }
        }
    }
}
