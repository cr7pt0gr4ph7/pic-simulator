using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Utils;

namespace PicSim.Instructions
{
    /// <summary>
    /// "Bit Set f" instruction.
    /// See "9.1 Instruction Descriptions - BSF", p.54
    /// </summary>
    /// <remarks>
    /// Bit 'b' in register 'f' is set.
    /// </remarks>
    public class BSF : BitOrientedInstruction
    {
        protected override void Execute(byte bit, byte address)
        {
            Processor.Memory[address] = Processor.Memory[address].SetBit(bit);
        }
    }
}
