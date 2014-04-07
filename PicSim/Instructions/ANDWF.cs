using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    /// <summary>
    /// "AND W with f" instruction.
    /// See "9.1 Instruction Descriptions - ANDWF", p.53
    /// </summary>
    public class ANDWF : BinaryByteOrientedInstruction
    {
        protected override AffectedStatus AffectedStatus
        {
            get { return AffectedStatus.Zero; }
        }

        protected override byte Compute(byte value)
        {
            return (byte)(W & value);
        }
    }
}
