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
    [Display("Marker", Group: "FieldFox.NA.N9918B", Description: "Insert a description here")]
    public class Marker : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N9918B MyInst { get; set; }

        [DisplayAttribute("InstMode", "", "Input Parameters", 2)]
        public string InstMode { get; set; } = "NA";

        [DisplayAttribute("MarkerNo", "", "Input Parameters", 2)]
        public uint MarkerNo { get; set; } = 1u;

        [DisplayAttribute("MarkerBWstate", "", "Input Parameters", 2)]
        public bool MarkerBWstate { get; set; } = true;

        [DisplayAttribute("MarkerBWThreshold", "", "Input Parameters", 2)]
        public double MarkerBWThreshold { get; set; } = 3D;

        [DisplayAttribute("MarkerFormat", "DEF - (Default)  Same as displayed format.\r\nIMPedance - R+jX format\r\nPHASe - Phas" +
            "e in degrees.\r\nZMAGnitude - Impedance Magnitude\r\nMAGPhase - Magnitude and Phase\r" +
            "\nREAL -\r\nIMAGinary - \r\nDBA -", "Input Parameters", 2)]
        public string MarkerFormat { get; set; } = "DEF";

        [DisplayAttribute("MarkerMode", "", "Input Parameters", 2)]
        public string MarkerMode { get; set; } = "OFF";

        [DisplayAttribute("TraceNum", "", "Input Parameters", 2)]
        public int TraceNum { get; set; } = 1;

        [DisplayAttribute("MarkerX", "", "Input Parameters", 2)]
        public double MarkerX { get; set; } = 0D;

        // Bandwidth, Center Frequency, Q, and Loss
        private Double[] MarkerBWdata;

        private Double MarkerMag;

        private Double MarkerPhase;

        #endregion

        public Marker()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.


            MyInst.ScpiCommand(":INSTrument:SELect {0}", InstMode);
            MyInst.ScpiCommand("*OPC");
            MyInst.ScpiCommand(":CALCulate:SELected:MARKer{0}:ACTivate", MarkerNo);
            MyInst.ScpiCommand(":CALCulate:SELected:MARKer{0}:AOFF", MarkerNo);
            // Marker BW
            MarkerBWdata = MyInst.ScpiQuery<System.Double[]>(Scpi.Format(":CALCulate:SELected:MARKer{0}:BWIDth:DATA?", MarkerNo), true);
            MyInst.ScpiCommand(":CALCulate:SELected:MARKer{0}:BWIDth:STATe {1}", MarkerNo, MarkerBWstate);
            MyInst.ScpiCommand(":CALCulate:SELected:MARKer{0}:FUNCtion:BWIDth:THReshold {1}", MarkerNo, MarkerBWThreshold);
            MyInst.ScpiCommand(":CALCulate:SELected:MARKer{0}:FORMat {1}", MarkerNo, MarkerFormat);
            MyInst.ScpiCommand(":CALCulate:SELected:MARKer{0}:STATe {1}", MarkerNo, MarkerMode);
            MyInst.ScpiCommand(":CALCulate:SELected:MARKer{0}:TRACe {1}", MarkerNo, TraceNum);
            MyInst.ScpiCommand(":CALCulate:SELected:MARKer{0}:X {1}", MarkerNo, MarkerX);
            MarkerPhase = MyInst.ScpiQuery<System.Double>(Scpi.Format(":CALCulate:SELected:MARKer{0}:Y?", MarkerNo), true);
            MyInst.ScpiCommand(":CALCulate:SELected:MARKer{0}:FUNCtion:MAXimum", MarkerNo);
            MyInst.ScpiCommand(":CALCulate:SELected:MARKer{0}:FUNCtion:MINimum", MarkerNo);
            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
