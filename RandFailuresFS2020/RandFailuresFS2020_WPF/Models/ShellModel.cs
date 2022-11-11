using SimConModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RandFailuresFS2020_WPF.Models
{
    public class ShellModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _toolTipEnabled;
        private bool _settingsEnabled;
        private bool _presetsEnabled;


        public bool ToolTipEnabled
        {
            get { return _toolTipEnabled; }
            set
            {
                _toolTipEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public bool SettingsEnabled
        {
            get { return _settingsEnabled; }
            set
            {
                _settingsEnabled = value;
                NotifyPropertyChanged();
            }
        }
        public bool PresetsEnabled
        {
            get { return _presetsEnabled; }
            set
            {
                _presetsEnabled = value;
                NotifyPropertyChanged();
            }
        }


        public ShellModel()
        {
            ToolTipEnabled = false;
            SettingsEnabled = true;
            PresetsEnabled = true;

            SimConHelper.GetSimConHelper().FailuresInProgressEvent += ShellModel_FailuresInProgressEvent;
        }

        private void ShellModel_FailuresInProgressEvent(object? sender, bool e)
        {
            if (e)
            {
                ToolTipEnabled = true;
                SettingsEnabled = false;
                PresetsEnabled = false;
            }
            else
            {
                ToolTipEnabled = false;
                SettingsEnabled = true;
                PresetsEnabled = true;
            }
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
