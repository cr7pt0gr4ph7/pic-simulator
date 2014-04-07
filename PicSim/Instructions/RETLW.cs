namespace PicSim.Instructions
{
    /// <summary>
    /// "Return with Literal in W" instruction.
    /// See "9.1 Instruction Descriptions - RETLW", p.62
    /// </summary>
    /// <remarks>
    /// The W register is loaded with the eight bit literal 'k'.
    /// The program counter is loaded from the top of the stack (the return address).
    /// This is a two cycle instruction.
    /// </remarks>
    public class RETLW : LiteralOrControlInstruction
    {
        protected override void Execute(byte immediateValue)
        {
            // Pop return address from the stack and load it into the PC
            PC.LoadFrom13Bits(Stack.Pop());

            // Load literal into the W register
            W = immediateValue;
        }
    }
}
