namespace PicSim.Instructions
{
    /// <summary>
    /// "Return from Subroutine" instruction.
    /// See "9.1 Instruction Descriptions - RETURN", p.62
    /// </summary>
    /// <remarks>
    /// Return from subroutine. The stack is  POPed and the top of the stack (TOS) 
    /// is loaded into the program counter. This is a two cycle instruction.
    /// </remarks>
    public class RETURN : Instruction
    {
        protected override void OnExecute(ushort opcode)
        {
            // Pop return address from the stack and load it into the PC
            PC.LoadFrom13Bits(Stack.Pop());
        }
    }
}
