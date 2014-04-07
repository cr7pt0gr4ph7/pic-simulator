using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Components;

namespace PicSim.Instructions
{
    /// <summary>
    /// "Bit Test f, Skip if Set" instruction.
    /// See "9.1 Instruction Descriptions - BTFSS", p.55
    /// </summary>
    public class BTFSS : BitOrientedInstruction
    {
        protected override void Execute(byte bit, byte address)
        {
            if (GetBit(bit, address)) {
                // Skip the next instruction if 'b' is set
                PC.LoadFrom13Bits((ushort)(PC.Value + 2));
            }
        }
    }
}
