namespace SimConModels
{
    public class OptionsModel
    {
        /// <summary>
        /// name of option
        /// </summary>
        public string OptionName { get; set; }
        /// <summary>
        /// value of option
        /// </summary>
        public string OptionValue { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public OptionsModel()
        {

        }

        /// <summary>
        /// constructor that takes value from database
        /// </summary>
        /// <param name="_name">
        /// name of option, will be searched in database to find value
        /// </param>
        public OptionsModel(string _name)
        {
            OptionName = _name;
            OptionValue = SQLOptions.LoadOptionValue(_name);
        }

        /// <summary>
        /// Sends update to database
        /// </summary>
        public void Update()
        {
            SQLOptions.UpdateOption(this);
        }
    }
}
