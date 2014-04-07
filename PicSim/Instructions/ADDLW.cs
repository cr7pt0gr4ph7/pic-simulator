using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    /// <summary>
    /// "Add Literal and W" instruction.
    /// See "9.1 Instruction Descriptions - ADDLW", p.53
    /// </summary>
    public class ADDLW : LiteralOrControlInstruction
    {
        protected override void Execute(byte immediateValue)
        {
            Operations.HandleAddOrSub(Processor, Operations.Add(W, immediateValue));
        }
    }
}
