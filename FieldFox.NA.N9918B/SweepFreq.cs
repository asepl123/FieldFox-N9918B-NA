// Author: MyName
// Copyright:   Copyright 2020 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace FieldFox.NA.N9918B
{
    [Display("SweepFreq", Group: "FieldFox.NA.N9918B", Description: "Insert a description here")]
    public class SweepFreq : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N9918B MyInst { get; set; }

        [DisplayAttribute("FreqCenter", "Value, MIN, MAX", "Input Parameters", 2)]
        public string FreqCenter { get; set; } = "MIN";

        [DisplayAttribute("FreqSpan", "", "Input Parameters", 2)]
        public string FreqSpan { get; set; } = "MIN";

        [DisplayAttribute("FreqStart", "", "Input Parameters", 2)]
        public string FreqStart { get; set; } = "MIN";

        [DisplayAttribute("FreqStop", "", "Input Parameters", 2)]
        public string FreqStop { get; set; } = "MAX";

        [DisplayAttribute("SweepTime", "", "Input Parameters", 2)]
        public double SweepTime { get; set; } = 1D;

        [DisplayAttribute("SweepPoints", "  2 to 10001", "Input Parameters", 2)]
        public int SweepPoints { get; set; } = 250;

        private Double[] FreqData;

        #endregion

        public SweepFreq()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":SENSe:FREQuency:CENTer {0}", FreqCenter);
            MyInst.ScpiCommand(":SENSe:FREQuency:SPAN {0}", FreqSpan);
            MyInst.ScpiCommand(":SENSe:FREQuency:STARt {0}", FreqStart);
            MyInst.ScpiCommand(":SENSe:FREQuency:STOP {0}", FreqStop);
            MyInst.ScpiCommand(":FORMat:DATA ASC,0");
            FreqData = MyInst.ScpiQuery<System.Double[]>(Scpi.Format(":SENSe:FREQuency:DATA?"), true);
            MyInst.ScpiCommand(":SENSe:SWEep:TIME {0}", SweepTime);
            MyInst.ScpiCommand(":SENSe:SWEep:POINts {0}", SweepPoints);

            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}