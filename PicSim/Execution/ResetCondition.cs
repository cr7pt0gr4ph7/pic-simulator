using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSim.Execution
{
    /// <summary>
    /// See "Table 8-3 RESET CONDITION FOR PROGRAM COUNTER AND THE STATUS REGISTER", p.39.
    /// </summary>
    public enum ResetCondition
    {
        MCLR_Reset,
        MCLR_Sleep,
        WDT_Reset
    }
}
