using PicSim.Components.Registers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.UI.Models
{
    public enum TristateMode
    {
        In = 1,
        Out = 2,
        InOut = In | Out
    }

    public class TristatePortRegisterModel
    {
        private readonly IRegister m_portRegister;
        private readonly IRegister m_trisRegister;

        /// <summary>
        /// Create a new instance of <see cref="TristatePortRegisterModel"/>.
        /// </summary>
        /// <param name="port">The port register, for example <c>PORTA</c>.</param>
        /// <param name="tris">The corresponding tristate register, for example <c>TRISA</c>.</param>
        public TristatePortRegisterModel(IRegister port, IRegister tris)
        {
            Ensure.ArgumentNotNull(port, "port");
            Ensure.ArgumentNotNull(tris, "tris");

            m_portRegister = port;
            m_trisRegister = tris;

        }
    }

    public struct BitInfo
    {

    }
}
