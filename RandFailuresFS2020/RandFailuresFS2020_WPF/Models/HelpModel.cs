namespace RandFailuresFS2020_WPF.Models
{
    public class HelpModel : BaseModel
    {
        private string? _helpText;
        public string? HelpText
        {
            get { return _helpText; }
            set
            {
                _helpText = value;
                NotifyPropertyChanged();
            }
        }

        public HelpModel()
        {
            HelpText = RandFailures_Resources.Resources.randFailuresHelp;
        }
    }
}
