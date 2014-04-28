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

        private RS232 myCon;
        private CommunicationData[] tris;
        private CommunicationData[] port;

        public Communication()
        {
            myCon = new RS232();
            tris = new CommunicationData[2];
            port = new CommunicationData[2];
        }

        public void start()
        {
            myCon.Open();
        }

        public void stop()
        {
            myCon.Close();
        }

        public void Sync()
        {
            for(uint i = 0; i < port.Length ; i++)
            {
                myCon.WriteValue(tris[i].getUpperNibble());
                myCon.WriteValue(tris[i].getLowerNibble());

                myCon.WriteValue(port[i].getUpperNibble());
                myCon.WriteValue(port[i].getLowerNibble());
                myCon.WriteValue(CR); // CR
            }

            uint j = 0;
            byte data = 0;
            do
            {
                data = (byte) myCon.ReadValue();
                if (data == CR)
                    break;
                port[j].setUpperNibble(data);
                data = (byte)myCon.ReadValue();
                port[j].setLowerNibble(data);
                j++;
            } while (data != CR);

        }
    }
}
