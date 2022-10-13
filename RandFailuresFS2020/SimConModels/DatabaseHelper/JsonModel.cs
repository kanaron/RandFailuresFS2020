using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimConModels.DatabaseHelper
{
    public class JsonModel
    {
        public int Version { get; set; }
        public List<SimVarModel> SimVars { get; set; }
        //"SimVars": [
        //  {
        //    "SimVarID": 1,
        //    "SimVarName": "",
        //    "SimVariable": "PLANE ALTITUDE",
        //    "Unit": "Feet",
        //    "Domain": "",
        //    "IsEvent": 0,
        //    "IsLeak": 0,
        //    "IsStuck": 0,
        //    "IsComplete": 0,
        //    "IsFailable": 0
        //  }
    }
}
