using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace PicSim.Components.Communication
{
    public class RS232 :
        ICommunication
    {
        private SerialPort serialPort;

        public RS232()
        {
            serialPort.PortName = "COM1";
            serialPort.BaudRate = 4800;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            /*serialPort.Handshake = Handshake.None;
            serialPort.RtsEnable = true;
            serialPort.DtrEnable = true;*/
        }


        public bool Open()
        {
            try
            {
                serialPort.Open();
                if (!serialPort.IsOpen)
                    throw new ApplicationException("Cannot open searialPort");
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("ERROR: RS232 " + e.Message);
                return false;
            }
        }

        public bool Close()
        {
            serialPort.Close();
            return !serialPort.IsOpen;
        }

        public bool Reset()
        {
            if (Close())
            {
                return Open();
            }
            else
            {
                return false;
            }
        }

        public uint Pull()
        {
            return (uint) serialPort.ReadByte();
        }

        public bool Push(uint _data)
        {
            serialPort.Write("" + _data);
            return true;
        }
    }
}
