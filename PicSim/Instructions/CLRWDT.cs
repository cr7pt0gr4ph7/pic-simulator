namespace PicSim.Instructions
{
    /// <summary>
    /// "Clear Watchdog Timer" instruction.
    /// See "9.1 Instruction Descriptions - CLRWDT", p.56
    /// </summary>
    public class CLRWDT : Instruction
    {
        protected override void OnExecute(ushort opcode)
        {
            // Clear WDT counter & prescaler
            Processor.Watchdog.Clear();

            // Clear !TO and !PD
            Processor.Registers.Status.NotTimeOut = true;
            Processor.Registers.Status.NotPowerDown = true;
        }
    }
}
