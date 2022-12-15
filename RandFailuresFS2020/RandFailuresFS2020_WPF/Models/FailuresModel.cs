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

            FilteredVarsList = PresetVarsList.Where(x => x.Domain!.Equals(DomainName)).ToList();
        }

        public void SaveVarsInPreset()
        {
            SQLSimVar.Delete(PresetID);
            SQLSimVar.Insert(PresetVarsList, PresetID);
        }
    }
}
