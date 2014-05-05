using NLog;
using PicSim.Components.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Communication
{
    public class CommunicationManager
    {
        private static readonly Logger ms_logger = LogManager.GetCurrentClassLogger();
        private const uint CARRIAGE_RETURN = 0x0D;

        private ICommunication m_connection;
        private CommPortInfo[] m_ports;

        public CommunicationManager(ICommunication commInterface)
        {
            Ensure.ArgumentNotNull(commInterface, "commInterface");
            m_connection = commInterface;

            var portA = new CommPortInfo();
            var portB = new CommPortInfo();

            PORTA = portA.ValueRegister;
            TRISA = portA.TrisRegister;

            PORTB = portB.ValueRegister;
            TRISB = portB.TrisRegister;

            m_ports = new CommPortInfo[] { portA, portB };
        }

        public IRegister PORTA { get; private set; }
        public IRegister PORTB { get; private set; }
        public IRegister TRISA { get; private set; }
        public IRegister TRISB { get; private set; }

        public void Start()
        {
            m_connection.Open();
        }

        public void Stop()
        {
            m_connection.Close();
        }

        /// <summary>
        /// Before executing the opcode: Pull the newest values from the serial interface.
        /// </summary>
        public void PreStep()
        {
            SendReceive();
        }

        /// <summary>
        /// After executing the opcode: Write the newest values to the serial interface.
        /// </summary>
        public void PostStep()
        {
            SendReceive();
        }

        private void SendReceive()
        {
            // Send
            foreach (CommPortInfo port in m_ports)
            {
                m_connection.WriteValue(port.TrisRegister.GetUpperNibble());
                m_connection.WriteValue(port.TrisRegister.GetLowerNibble());

                m_connection.WriteValue(port.ValueRegister.GetUpperNibble());
                m_connection.WriteValue(port.ValueRegister.GetLowerNibble());
            }
            m_connection.WriteValue(CARRIAGE_RETURN);

            // Receive
            uint j = 0;
            byte data = 0;
            do
            {
                // Read upper nibble
                data = (byte)m_connection.ReadValue();
                if (data == CARRIAGE_RETURN) break;

                if (j < m_ports.Length) m_ports[j].ValueRegister.SetUpperNibble(data);
                else ms_logger.Warn("Skipping out-of-range port {0}", j);

                // Read lower nibble
                data = (byte)m_connection.ReadValue();
                if (j < m_ports.Length) m_ports[j].ValueRegister.SetLowerNibble(data);
                else /* Warning was already written above */;

                j++;
            } while (data != CARRIAGE_RETURN);

            if (j < m_ports.Length) ms_logger.Warn("Only ports upto port {0} could be read from the serial port.", j - 1);

        }
    }
}
