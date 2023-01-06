using SimConModels;

namespace RandFailuresFS2020_WPF.Models
{
    public class SettingsModel : BaseModel
    {
        private PresetModel? _preset;
        private int _setAllPercent;
        private bool _showPopup;
        private string? _popupText;

        public string PopupText
        {
            get { return _popupText; }
            set
            {
                _popupText = value;
                NotifyPropertyChanged();
            }
        }
        public bool ShowPopup
        {
            get { return _showPopup; }
            set
            {
                _showPopup = value;
                NotifyPropertyChanged();
            }
        }
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
            ShowPopup = false;
        }

        public void Reload()
        {
            Preset = SQLPresets.LoadPreset(SQLOptions.LoadOptionValueInt("PresetID"));
            SetAllPercent = -1;
        }

        public void SavePreset()
        {
            if (ValidatePreset())
            {
                SQLPresets.UpdatePreset(Preset);

                PopupText = "Preset settings saved";
                ShowPopup = true;

                if (SetAllPercent >= 0)
                    SQLSimVar.UpdateAllPercentage(Preset.PresetID, SetAllPercent);
            }
            else
            {
                PopupText = "Data validation error. Min value can't be greater than Max value";
                ShowPopup = true;
            }
        }

        public void HidePopup()
        {
            PopupText = "";
            ShowPopup = false;
        }

        private bool ValidatePreset()
        {
            if (Preset.SpeedEnabled == 1 && Preset.SpeedMin > Preset.SpeedMax)
                return false;

            if (Preset.AltEnabled == 1 && Preset.AltMin > Preset.AltMax)
                return false;

            if (Preset.TimeEnabled == 1 && Preset.TimeMin > Preset.TimeMax)
                return false;

            return true;
        }
    }
}
