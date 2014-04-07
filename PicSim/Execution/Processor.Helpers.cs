using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Instructions;

namespace PicSim.Execution
{
    public partial class Processor
    {
        public void LoadProgram(ushort[] program)
        {
            Ensure.ArgumentNotNull(program, "program");
            this.ProgramMemory = program;
        }

        public void CheckZero(byte value)
        {
            Registers.Status.Zero = (value == 0);
        }

        public void SetStatus(OperationResult result)
        {
            Registers.Status.Zero = result.Zero;
            Registers.Status.Carry = result.Carry;
            Registers.Status.DigitCarry = result.DigitCarry;
        }
    }
}
