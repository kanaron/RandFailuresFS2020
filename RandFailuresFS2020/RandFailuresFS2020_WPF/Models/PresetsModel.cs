using SimConModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RandFailuresFS2020_WPF.Models
{
    public class PresetsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private List<PresetModel>? _presetsList;
        private PresetModel? _selectedPreset;
        private List<SimVarModel>? _presetVarsList;
        private int _selectedIndexPreset;


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
                LoadVarsList();
            }
        }
        public int SelectedIndexPreset
        {
            get { return _selectedIndexPreset; }
            set
            {
                _selectedIndexPreset = value;
                NotifyPropertyChanged();
            }
        }
        public List<SimVarModel> PresetVarsList
        {
            get { return _presetVarsList!; }
            set
            {
                _presetVarsList = value;
                NotifyPropertyChanged();
            }
        }



        public PresetsModel()
        {
            PresetsList = SQLPresets.LoadPresets();
            SelectedIndexPreset = PresetsList.FindIndex(a => a.PresetID.Equals(SQLOptions.LoadOptionValueInt("PresetID")));
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void DeletePreset()
        {
            if (SelectedPreset.PresetID != 1)
            {
                SQLPresets.Delete(SelectedPreset);
                PresetsList = SQLPresets.LoadPresets();
                SelectedPreset = PresetsList[0];
            }
        }

        public void NewPreset(string name)
        {
            SQLPresets.Insert(name);
            PresetsList = SQLPresets.LoadPresets();
            SelectedPreset = PresetsList[^1];
        }

        public void LoadVarsList()
        {
            //if (SelectedPreset != null)
                PresetVarsList = SQLSimVar.LoadFailableSimVarsList(SelectedPreset.PresetID);
        }

        public void SaveVarsInPreset()
        {
            SQLSimVar.Delete(SelectedPreset.PresetID);
            SQLSimVar.Insert(PresetVarsList, SelectedPreset.PresetID);
        }

        public void Reload()
        {
            SelectedIndexPreset = PresetsList.FindIndex(a => a.PresetID.Equals(SQLOptions.LoadOptionValueInt("PresetID")));
            LoadVarsList();
        }
    }
}
