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
    }
}
