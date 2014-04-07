using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    /// <summary>
    /// "Move Literal to W" instruction.
    /// See "9.1 Instruction Descriptions - MOVLW", p.60
    /// </summary>
    public class MOVLW : LiteralOrControlInstruction
    {
        protected override void Execute(byte immediateValue)
        {
            W = immediateValue;
        }   
    }
}
