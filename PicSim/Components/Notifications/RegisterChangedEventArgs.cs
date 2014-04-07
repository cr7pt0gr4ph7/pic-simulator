using System;

namespace PicSim.Components.Notifications
{
    public class RegisterChangedEventArgs : EventArgs
    {
        private readonly byte m_newValue;

        public RegisterChangedEventArgs(byte newValue)
        {
            m_newValue = newValue;
        }

        public byte NewValue
        {
            get { return m_newValue; }
        }
    }
}
