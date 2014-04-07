using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    /// <summary>
    /// "Move W to f" instruction.
    /// See "9.1 Instruction Descriptions - MOVWF", p.60
    /// </summary>
    /// <remarks>
    /// Move data from W register to register 'f'.
    /// </remarks>
    public class MOVWF : ByteOrientedInstruction
    {
        protected override void Execute(bool destination, byte address)
        {
            if (!destination) throw new InvalidOperationException("Destination bit should be 1");

            // Write the contents of the W register to 'f'
            Processor.Memory[address] = W;
        }
    }
}
