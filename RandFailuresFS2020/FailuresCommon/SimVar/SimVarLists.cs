using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FailuresCommon
{
    public class SimVarLists
    {
        private static readonly SimVarLists instance = new SimVarLists();
        public int Preset { get; set; }

        private List<SimVarModel> SimVarDataList;
        private List<SimVarModel> SimVarFailableList;
        private List<SimVarModel> SimVarWillFailList;

        private OptionsModel PresetOption;

        private SimVarLists()
        {
            PresetOption = new OptionsModel("PresetID");
            if (PresetOption.OptionValue != "")
            {
                int presetParse;
                int.TryParse(PresetOption.OptionValue, out presetParse);
                Preset = presetParse;
            }
            else
            {
                Preset = 1;
            }
        }

        public static SimVarLists GetSimVarLists()
        {
            return instance;
        }

        public void LoadLists()
        {
            PresetOption.OptionValue = Preset.ToString();
            PresetOption.Update();


        }
    }
}
