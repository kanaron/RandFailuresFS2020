using SimConModels;
using System.Collections.Generic;

namespace RandFailuresFS2020_WPF.Models
{
    public class PresetsModel : BaseModel
    {
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
            if (SelectedPreset != null)
                PresetVarsList = SQLSimVar.LoadFailableSimVarsList(SelectedPreset.PresetID);
        }

        public void Reload()
        {
            LoadVarsList();
        }
    }
}
