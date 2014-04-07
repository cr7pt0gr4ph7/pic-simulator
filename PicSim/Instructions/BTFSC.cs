using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    /// <summary>
    /// "Bit Test f, Skip if Clear" instruction.
    /// See "9.1 Instruction Descriptions - BTFSC", p.54
    /// </summary>
    public class BTFSC : BitOrientedInstruction
    {
        protected override void Execute(byte bit, byte address)
        {
            if (!GetBit(bit, address)) {
                // Skip the next instruction if 'b' is zero
                PC.LoadFrom13Bits((ushort)(PC.Value + 2));
            }
        }
    }
}
