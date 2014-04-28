using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using NLog;
using System.Diagnostics;

namespace PicSim.Components.Communication
{
    public class RS232 : ICommunication
    {
        private static readonly Logger ms_logger = LogManager.GetCurrentClassLogger();
        private SerialPort m_serialPort;

        public RS232()
        {
            m_serialPort = new SerialPort("COM1", 4800, Parity.None, 8, StopBits.One);
            /*serialPort.Handshake = Handshake.None;
            serialPort.RtsEnable = true;
            serialPort.DtrEnable = true;*/
        }

        /// <summary>
        /// </summary>
        /// <returns><c>true</c> on success; <c>false</c> on failure.</returns>        
        public bool Open()
        {
            ms_logger.Info("Opening the serial port...");
            try
            {
                m_serialPort.Open();
                if (!m_serialPort.IsOpen)
                    throw new ApplicationException("Cannot open the serial port");
                ms_logger.Info("Connection established.");
                return true;
            }
            catch (Exception e)
            {
                ms_logger.ErrorException("Exception in Open(): " + e.Message, e);
                return false;
            }
        }

        /// <summary>
        /// </summary>
        /// <returns><c>true</c> on success; <c>false</c> on failure.</returns>
        public bool Close()
        {
            ms_logger.Info("Closing the serial port...");
            m_serialPort.Close();
            ms_logger.Info("Serial port has been closed.");

            Debug.Assert(!m_serialPort.IsOpen);
            return !m_serialPort.IsOpen;
        }

        /// <summary>
        /// Close and reopen the port if it is already open.
        /// Do nothing if it is not open.
        /// </summary>
        /// <returns><c>true</c> on success; <c>false</c> on failure.</returns>
        public bool Reset()
        {
            if (Close()) return Open();
            else return false;
        }

        public uint ReadValue()
        {
            return (uint)m_serialPort.ReadByte();
        }

        public bool WriteValue(uint _data)
        {
            m_serialPort.Write(_data.ToString());
            return true;
        }
    }
}
