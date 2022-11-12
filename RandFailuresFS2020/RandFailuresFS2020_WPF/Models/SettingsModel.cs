using SimConModels;

namespace RandFailuresFS2020_WPF.Models
{
    public class SettingsModel : BaseModel
    {
        private PresetModel? _preset;
        private int _setAllPercent;

        public int SetAllPercent
        {
            get { return _setAllPercent; }
            set
            {
                _setAllPercent = value;
                NotifyPropertyChanged();
            }
        }
        public PresetModel Preset
        {
            get { return _preset!; }
            set
            {
                _preset = value;
                NotifyPropertyChanged();
            }
        }

        public SettingsModel()
        {
            Preset = SQLPresets.LoadPreset(SQLOptions.LoadOptionValueInt("PresetID"));
        }

        public void Reload()
        {
            Preset = SQLPresets.LoadPreset(SQLOptions.LoadOptionValueInt("PresetID"));
            SetAllPercent = 0;
        }

        public void SavePreset()
        {
            SQLPresets.UpdatePreset(Preset);
            SQLSimVar.UpdateAllPercentage(Preset.PresetID, SetAllPercent);
        }
    }
}
