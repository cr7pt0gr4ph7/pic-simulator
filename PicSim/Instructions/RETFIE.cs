using System;

namespace PicSim.Instructions
{
    /// <summary>
    /// "Return from Interrupt" instruction.
    /// See "9.1 Instruction Descriptions - RETFIE", p.61
    /// </summary>
    /// <remarks><![CDATA[
    /// Return from Interrupt. Stack is POPed and Top of Stack (TOS) is loaded into the program counter.
    /// Interrupts are enabled by setting Global Interrupt Enable bit, GIE (INTCON<7>).
    /// This is a two cycle instruction.
    /// ]]></remarks>
    public class RETFIE : Instruction
    {
        protected override void OnExecute(ushort opcode)
        {
            // Pop return address from stack and load it into the PC
            PC.LoadFrom13Bits(Stack.Pop());

            // Reenable the GIE bit
            throw new NotImplementedException();
        }
    }
}
