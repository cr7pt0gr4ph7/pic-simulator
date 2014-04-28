using PicSim.Components.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Components.Communication
{
    /// <summary>
    /// This class models a communcation port (PORTA, PORTB, ...) which has a value register and a tristate register.
    /// </summary>
    public class CommPortInfo
    {
        private readonly IRegister m_valueRegister;
        private readonly IRegister m_trisRegister;

        public CommPortInfo()
        {
            m_valueRegister = new MemoryCell();
            m_trisRegister = new MemoryCell();
        }

        public IRegister ValueRegister
        {
            get { return m_valueRegister; }
        }

        public IRegister TrisRegister
        {
            get { return m_trisRegister; }
        }
    }
}
