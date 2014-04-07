using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Registers
{
    /// <summary>
    /// A readable and writable memory cell.
    /// </summary>
    public class MemoryCell : IRegister
    {
        private byte m_value;

        public byte Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
    }
}
