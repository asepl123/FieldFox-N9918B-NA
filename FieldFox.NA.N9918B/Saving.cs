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
    [Display("Saving", Group: "FieldFox.NA.N9918B", Description: "Insert a description here")]
    public class Saving : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public N9918B MyInst { get; set; }


        [DisplayAttribute("CurrDirectory", "", "Input Parameters", 2)]
        public string CurrDirectory { get; set; } = "temp";

        [DisplayAttribute("filename", "", "Input Parameters", 2)]
        public string Filename { get; set; } = "abc.csv";

        [DisplayAttribute("data", "", "Input Parameters", 2)]
        public byte[] Data { get; set; } = new byte[] {
                35,
                50,
                48,
                50,
                49,
                50,
                51};

        [DisplayAttribute("MakeDirectory", "", "Input Parameters", 2)]
        public string MakeDirectory { get; set; } = "temp";

        [DisplayAttribute("SnpFile", "", "Input Parameters", 2)]
        public string SnpFile { get; set; } = "abc.s2p";

        [DisplayAttribute("StateFile", "", "Input Parameters", 2)]
        public string StateFile { get; set; } = "state.sta";

        [DisplayAttribute("ImgFile", "", "Input Parameters", 2)]
        public string ImgFile { get; set; } = "abc.png";

        [DisplayAttribute("DataFile", "", "Input Parameters", 2)]
        public string DataFile { get; set; } = "file.csv";


        #endregion

        public Saving()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.


            MyInst.ScpiCommand(":MMEMory:CDIRectory {0}", CurrDirectory);
            MyInst.ScpiIEEEBlockCommand(string.Format(":MMEMory:DATA {0}", Filename), MyInst.GetBytes(Data));
            MyInst.ScpiCommand(":MMEMory:MDIRectory {0}", MakeDirectory);
            MyInst.ScpiCommand(":MMEMory:STORe:FDATa {0}", DataFile);
            MyInst.ScpiCommand(":MMEMory:STORe:IMAGe {0}", ImgFile);
            MyInst.ScpiCommand(":MMEMory:STORe:SNP:DATA {0}", SnpFile);
            MyInst.ScpiCommand(":MMEMory:STORe:STATe {0}", StateFile);

            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
