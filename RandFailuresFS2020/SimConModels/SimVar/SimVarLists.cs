using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimConModels
{
    public class SimVarLists
    {
        private static readonly SimVarLists instance = new SimVarLists();
        //public int PresetID { get; private set; }

        private List<SimVarModel> SimVarDataList;
        private List<SimVarModel> SimVarFailableList;
        private List<SimVarModel> SimVarFailuresList;

        //private OptionsModel PresetOption;

        //public event EventHandler<List<SimVarModel>> ListsLoaded;

        private SimVarLists()
        {

        }

        public static SimVarLists GetSimVarLists()
        {
            return instance;
        }

        public void LoadLists(int presetID)
        {
            SimVarFailableList = SQLSimVar.LoadFailableSimVarsList(presetID, true);
            SimVarDataList = SQLSimVar.LoadDataList();
            FillSimVarEnums(SimVarDataList);
            FillSimVarEnums(SimVarFailableList);

            SimCon.GetSimCon().RegisterList(SimVarDataList);
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

        public List<SimVarModel> GetDataList() => SimVarDataList;
    }
}
