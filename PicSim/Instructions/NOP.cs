using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    // TODO Documentation for NOP
    public class NOP : Instruction
    {
        protected override void OnExecute(ushort opcode)
        {
            // Do nothing.
        }
    }
}
