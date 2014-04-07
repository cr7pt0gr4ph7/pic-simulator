using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    /// <summary>
    /// "AND Literal with W" instruction.
    /// See "9.1 Instruction Descriptions - ANDLW", p.53
    /// </summary>
    public class ANDLW : LiteralOrControlInstruction
    {
        protected override void Execute(byte immediateValue)
        {
            W &= immediateValue;
            Processor.CheckZero(Processor.W);
        }   
    }
}
