
namespace PicSim.Components.Communication
{
    /// <summary>
    /// An implementation of <see cref="ICommunication"/> that behaves like <c>/dev/null</c> in Linux-like operating systems.
    /// </summary>
    public class NullCommunication : ICommunication
    {
        private const uint CARRIAGE_RETURN = 0x0D;

        private readonly uint m_numOfPorts = 2;
        private uint m_curPort = 0;
        private bool m_isOpen;

        public bool Open()
        {
            if (m_isOpen)
            {
                return false;
            }
            else
            {
                m_isOpen = true;
                return true;
            }
        }

        public bool Close()
        {
            if (m_isOpen)
            {
                m_isOpen = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Reset()
        {
            if (Close())
                return Open();
            else
                return false;
        }

        public uint ReadValue()
        {
            if (m_curPort < m_numOfPorts)
            {
                m_curPort++;
                return 0;
            }
            else
            {
                m_curPort = 0;
                return CARRIAGE_RETURN;
            }
        }

        public bool WriteValue(uint _data)
        {
            // Ignore the argument value
            return true;
        }
    }
}
