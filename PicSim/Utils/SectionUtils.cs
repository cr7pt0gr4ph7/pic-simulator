using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Utils
{
    public static class SectionUtils
    {
        private static short MaxValueForBitCount(short bitCount)
        {
            return (short)((1 << bitCount) - 1);
        }

        public static BitVector32.Section ForRange(short startBit, short endBit)
        {
            if (startBit > endBit) {
                var tmp = startBit;
                startBit = endBit;
                endBit = tmp;
            }

            if (startBit == 0) {
                BitVector32.Section ourSection = BitVector32.CreateSection(MaxValueForBitCount(endBit));
                return ourSection;
            } else {
                BitVector32.Section previousRange = BitVector32.CreateSection(MaxValueForBitCount(startBit));
                BitVector32.Section ourSection = BitVector32.CreateSection(MaxValueForBitCount((short)(endBit - startBit + 1)), previousRange);
                return ourSection;
            }
        }

        public static int MaskForRange(short startBit, short endBit)
        {
            var mask = ForRange(startBit, endBit);
            return (mask.Mask << mask.Offset);
        }
    }
}
