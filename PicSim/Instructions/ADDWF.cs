using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Instructions
{
    /// <summary>
    /// "Add W and f" instruction.
    /// See "9.1 Instruction Descriptions - ADDWF", p.53
    /// </summary>
    public class ADDWF : ByteOrientedInstruction
    {
        protected override void Execute(bool destination, byte address)
        {
            Operations.HandleAddOrSub(Processor, Operations.Add(W, LoadAddress(address)), destination, address);
        }
    }
}
