using NLog;
using PicSim.Components.Registers;
using PicSim.Utils;
using System;

namespace PicSim.Components.Communication
{
    public class CommunicationManager
    {
        private static readonly Logger ms_logger = LogManager.GetCurrentClassLogger();
        private const uint CARRIAGE_RETURN = 0x0D;

        private ProxyCommunication m_connection;
        private CommPortInfo[] m_ports;

        public CommunicationManager()
        {
            m_connection = new ProxyCommunication();
            var portA = new CommPortInfo();
            var portB = new CommPortInfo();

            PORTA = portA.ValueRegister;
            TRISA = portA.TrisRegister;

            PORTB = portB.ValueRegister;
            TRISB = portB.TrisRegister;

            m_ports = new CommPortInfo[] { portA, portB };
        }

        public void SetUnderlying(Func<ICommunication> commInterface)
        {
            Ensure.ArgumentNotNull(commInterface, "commInterface");
            m_connection.SetUnderlying(commInterface);
        }

        public IRegister PORTA { get; private set; }
        public IRegister PORTB { get; private set; }
        public IRegister TRISA { get; private set; }
        public IRegister TRISB { get; private set; }

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
            // Only sync with next instruction
            // SendReceive();
        }

        private void SendReceive()
        {
            try
            {
                if (m_connection.IsConnected)
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
                        byte upperNibble = ((byte)0).SetUpperNibble(data.GetLowerNibble());

                        // Read lower nibble
                        data = (byte)m_connection.ReadValue();
                        byte lowerNibble = data.GetLowerNibble();
                        byte fullData = (byte)(upperNibble | lowerNibble);

                        if (j >= m_ports.Length) ms_logger.Warn("Skipping out-of-range port {0}", j);
                        else m_ports[j].RealInput = fullData;

                        j++;
                    } while (data != CARRIAGE_RETURN);

                    if (j < m_ports.Length) ms_logger.Warn("Only ports upto port {0} could be read from the serial port.", j - 1);
                }
            }
            catch (TimeoutException ex)
            {
                ms_logger.Error("COM-Port: Read or write timed out");
            }
        }
    }
}
