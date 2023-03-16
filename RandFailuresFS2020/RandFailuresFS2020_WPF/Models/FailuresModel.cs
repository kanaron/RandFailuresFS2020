using SimConModels;
using System.Collections.Generic;
using System.Linq;

namespace RandFailuresFS2020_WPF.Models
{
    public class FailuresModel : BaseModel
    {
        private List<SimVarModel>? _presetVarsList;
        private List<SimVarModel>? _filteredVarsList;
        private string? _domainName;
        private string? _setAllPercentageToText;
        private string? _setAllPercentageTextBox;
        private int _presetID;

        public int PresetID
        {
            get { return _presetID; }
            set { _presetID = value; }
        }
        public string DomainName
        {
            get { return _domainName!; }
            set
            {
                _domainName = value;
                NotifyPropertyChanged();
            }
        }
        public string SetAllPercentageToText
        {
            get { return _setAllPercentageToText!; }
            set
            {
                _setAllPercentageToText = value;
                NotifyPropertyChanged();
            }
        }
        public string SetAllPercentageTextBox
        {
            get { return _setAllPercentageTextBox!; }
            set
            {
                _setAllPercentageTextBox = value;
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
        public List<SimVarModel> FilteredVarsList
        {
            get { return _filteredVarsList!; }
            set
            {
                _filteredVarsList = value;
                NotifyPropertyChanged();
            }
        }

        public FailuresModel(string _domain, List<SimVarModel> _simVarList, int _presetID)
        {
            DomainName = _domain;
            PresetVarsList = _simVarList;
            PresetID = _presetID;
            SetAllPercentageToText = "Set all ‰ for " + DomainName + " to:";

            LoadList();
        }

        public void SaveVarsInPreset()
        {
            foreach (var simVar in FilteredVarsList)
            {
                int index = PresetVarsList.FindIndex(x => x.SimVarID == simVar.SimVarID);
                PresetVarsList[index] = simVar;
            }

            SQLSimVar.Delete(PresetID);
            SQLSimVar.Insert(PresetVarsList, PresetID);
        }

        public void SettAllPercentage()
        {
            foreach (var simVar in FilteredVarsList)
            {
                int index = PresetVarsList.FindIndex(x => x.SimVarID == simVar.SimVarID);
                PresetVarsList[index] = simVar;
                int.TryParse(SetAllPercentageTextBox, out int val);
                PresetVarsList[index].FailPercent = val;
            }

            SQLSimVar.Delete(PresetID);
            SQLSimVar.Insert(PresetVarsList, PresetID);

            Reload();
        }

        public void LoadList()
        {
            FilteredVarsList = PresetVarsList.Where(x => x.Domain!.Equals(DomainName)).ToList();
        }

        public void Reload()
        {
            LoadList();
        }
    }
}
