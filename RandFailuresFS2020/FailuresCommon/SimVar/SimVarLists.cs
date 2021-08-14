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

        private SimVarLists()
        {

        }

        public static SimVarLists GetSimVarLists()
        {
            return instance;
        }
    }
}
