namespace SimConModels.DatabaseHelper
{
    public class JsonModel
    {
        public int Version { get; set; }
        public List<SimVarModel>? SimVars { get; set; }
    }
}
