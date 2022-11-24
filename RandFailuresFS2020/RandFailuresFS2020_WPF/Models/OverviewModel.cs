using SimConModels;
using System.Collections.Generic;
using System.Windows.Media;

namespace RandFailuresFS2020_WPF.Models
{
    public class OverviewModel : BaseModel
    {
        private string? _stateText;
        private Brush? _stateColor;
        private bool _resetEnabled;
        private bool _startStopEnabled;
        private List<PresetModel>? _presetsList;
        private PresetModel? _selectedPreset;
        private int _selectedItemPreset;
        private string? _startStopText;
        private Brush? _startStopTextColor;


        public List<PresetModel> PresetsList
        {
            get { return _presetsList!; }
            set
            {
                _presetsList = value;
                NotifyPropertyChanged();
            }
        }
        public PresetModel SelectedPreset
        {
            get { return _selectedPreset!; }
            set
            {
                _selectedPreset = value;
                NotifyPropertyChanged();
                if (value != null)
                    SQLOptions.UpdateOption("PresetID", value.PresetID.ToString());
            }
        }
        public int SelectedItemPreset
        {
            get { return _selectedItemPreset; }
            set
            {
                _selectedItemPreset = value;
                NotifyPropertyChanged();
            }
        }
        public string StateText
        {
            get { return _stateText!; }
            set
            {
                _stateText = value;
                NotifyPropertyChanged();
            }
        }
        public Brush StateColor
        {
            get { return _stateColor!; }
            set
            {
                _stateColor = value;
                NotifyPropertyChanged();
            }
        }
        public bool ResetEnabled
        {
            get { return _resetEnabled; }
            set
            {
                _resetEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public bool StartStopEnabled
        {
            get { return _startStopEnabled; }
            set
            {
                _startStopEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public string StartStopText
        {
            get { return _startStopText; }
            set
            {
                _startStopText = value;
                NotifyPropertyChanged();
            }
        }
        public Brush StartStopTextColor
        {
            get { return _startStopTextColor!; }
            set
            {
                _startStopTextColor = value;
                NotifyPropertyChanged();
            }
        }

        public OverviewModel()
        {
            SimCon.GetSimCon().StateChanged += OverviewPresenter_StateChanged;
            StateText = "Sim not found";
            StateColor = Brushes.Red;
            ResetEnabled = false;
            StartStopEnabled = false;
            StartStopText = "Start";
            StartStopTextColor = Brushes.Green;
            Reload();
        }

        public void Reload()
        {
            PresetsList = SQLPresets.LoadPresets();
            SelectedItemPreset = PresetsList.FindIndex(a => a.PresetID.Equals(SQLOptions.LoadOptionValueInt("PresetID")));
        }

        private void OverviewPresenter_StateChanged(object? sender, string e)
        {
            StateText = e;
            switch (e)
            {
                case "Sim not found":
                    {
                        StateColor = Brushes.Red;
                        StartStopEnabled = false;
                        ResetEnabled = false;
                        break;
                    }
                case "Sim connected":
                    {
                        StateColor = Brushes.Blue;
                        StartStopEnabled = true;
                        ResetEnabled = true;
                        StartStopText = "Start";
                        break;
                    }
                case "Failures started":
                    {
                        StateColor = Brushes.Green;
                        ResetEnabled = true;
                        StartStopEnabled = true;
                        StartStopText = "Stop";
                        break;
                    }
                case "Failures stopped":
                    {
                        StateColor = Brushes.Red;
                        ResetEnabled = true;
                        StartStopEnabled = true;
                        StartStopText = "Start";
                        break;
                    }
            }
        }

        public void StartStopClicked()
        {
            if (StartStopText == "Start")
            {
                SimConHelper.GetSimConHelper().ManageFailTimer(true);
                StartStopText = "Stop";
                StartStopTextColor = Brushes.Red;
            }
            else
            {
                SimConHelper.GetSimConHelper().ManageFailTimer(false);
                StartStopText = "Start";
                StartStopTextColor = Brushes.Green;
            }
        }
    }
}
