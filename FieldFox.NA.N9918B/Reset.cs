using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTap;   // Use OpenTAP infrastructure/core components (log,TestStep definition, etc)

namespace FieldFox.NA.N9918B
{
    [Display("Reset", Group: "FieldFox.NA.N9918B", Description: "Insert description here")]
    public class Reset : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N9918B MyInst { get; set; }

        #endregion

        public Reset()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand("*RST");
            MyInst.ScpiCommand(":SYSTem:PRESet");
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
