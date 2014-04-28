using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Communication
{
    class Communication
    {
        const uint CR = 0x0D;

        private RS232 m_backingPort;
        private CommunicationData[] tris;
        private CommunicationData[] port;

        public Communication()
        {
            m_backingPort = new RS232();
            tris = new CommunicationData[2];
            port = new CommunicationData[2];
        }

        public void start()
        {
            m_backingPort.Open();
        }

        public void stop()
        {
            m_backingPort.Close();
        }

        public void Sync()
        {
            for(uint i = 0; i < port.Length ; i++)
            {
                m_backingPort.WriteValue(tris[i].GetUpperNibble());
                m_backingPort.WriteValue(tris[i].GetLowerNibble());

                m_backingPort.WriteValue(port[i].GetUpperNibble());
                m_backingPort.WriteValue(port[i].GetLowerNibble());
                m_backingPort.WriteValue(CR); // CR
            }

            uint j = 0;
            byte data = 0;
            do
            {
                data = (byte) m_backingPort.ReadValue();
                if (data == CR)
                    break;
                port[j].SetUpperNibble(data);
                data = (byte)m_backingPort.ReadValue();
                port[j].SetLowerNibble(data);
                j++;
            } while (data != CR);

        }
    }
}
