namespace SimConModels
{
    public class PresetModel
    {
        public int PresetID { get; private set; }

        public string? PresetName { get; set; }
        public int SpeedEnabled { get; set; } = 1;
        public int SpeedMin { get; set; } = 50;
        public int SpeedMax { get; set; } = 150; 
        public int AltEnabled { get; set; } = 1;
        public int AltMin { get; set; } = 2000; 
        public int AltMax { get; set; } = 10000;
        public int TimeEnabled { get; set; } = 1;
        public int TimeMin { get; set; } = 20; 
        public int TimeMax { get; set; } = 3600;
        public int InstantEnabled { get; set; } = 1;
        public int NoOfMaxPossibleFailures { get; set; } = 99;
    }
}
