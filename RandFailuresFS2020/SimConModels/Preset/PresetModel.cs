namespace SimConModels
{
    public class PresetModel
    {
        public int PresetID { get; private set; }

        public string PresetName { get; set; }
        public int SpeedEnabled { get; set; }
        public int SpeedMin { get; set; }
        public int SpeedMax { get; set; }
        public int AltEnabled { get; set; }
        public int AltMin { get; set; }
        public int AltMax { get; set; }
        public int TimeEnabled { get; set; }
        public int TimeMin { get; set; }
        public int TimeMax { get; set; }
        public int InstantEnabled { get; set; }
    }
}
