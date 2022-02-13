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
        public int PresetID { get; set; }

        private List<SimVarModel> SimVarDataList;
        private List<SimVarModel> SimVarFailableList;
        private List<SimVarModel> SimVarFailuresList;

        private OptionsModel PresetOption;

        public event EventHandler<List<SimVarModel>> ListsLoaded;

        private SimVarLists()
        {
            PresetOption = new OptionsModel("PresetID");
            if (PresetOption.OptionValue != "")
            {
                int presetParse;
                int.TryParse(PresetOption.OptionValue, out presetParse);
                PresetID = presetParse;
            }
            else
            {
                PresetID = 1;
            }
        }

        public static SimVarLists GetSimVarLists()
        {
            return instance;
        }

        public void LoadLists()
        {
            PresetOption.OptionValue = PresetID.ToString();
            PresetOption.Update();

            SimVarFailableList = SQLSimVar.LoadFailableSimVarsList(PresetID, true);
            FillSimVarEnums(SimVarFailableList);

            ListsLoaded?.Invoke(this, SimVarFailableList);
        }

        private void FillSimVarEnums(List<SimVarModel> list)
        {
            foreach (SimVarModel simVarModel in list)
            {
                simVarModel.FillEnums();
            }
        }

        public List<SimVarModel> GetFailableList()
        {
            return SimVarFailableList;
        }
    }
}
