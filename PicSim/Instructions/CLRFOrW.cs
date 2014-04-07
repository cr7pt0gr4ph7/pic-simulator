using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    /// <summary>
    /// "Clear f" and "Clear W" instruction.
    /// See "9.1 Instruction Descriptions - CLRF / CLRW", p.56
    /// </summary>
    /// <remarks>
    /// This instruction merges the <c>CLRF</c> and <c>CLRW</c> instructions.
    /// </remarks>
    public class CLRFOrW : BinaryByteOrientedInstruction
    {
        protected override bool NeedsInputValue
        {
            get { return false; }
        }

        protected override byte Compute(byte value)
        {
            return 0;
        }
    }
}
