using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicSim.Execution;

namespace PicSim
{
    class Program
    {
		//TODO remove
        static void Main(string[] args)
        {
            Processor processor = new Processor();
            processor.LoadProgram(GetBinaryCounter());

            while (true) {
                Console.WriteLine("PC: {0}", processor.ProgramCounter.Value);
                Console.ReadKey();

                processor.Step();
            }
        }

        static ushort[] GetBinaryCounter()
        {
            return new ushort[] {
                0x0186,
                0x1683,
                0x3000,
                0x0086,
                    
                0x0185,
                    
                0x3007,
                0x0085,
                                        
                0x1283,
                0x1703,

                0x0190,

                0x1C85,
                0x280E,
                0x0186,
                0x280A,
                    
                0x1905,
                0x280A,
                    
                0x0805,
                0x3901,
                0x0690,
                0x1810,
                0x2817,
                0x0090,
                0x280A,
                    
                0x0090,
                0x0A86,
                0x1903,
                0x281D,
                0x1185,
                0x280A,
                    
                0x1585,
                0x280A,
            };
        }
    }
}
