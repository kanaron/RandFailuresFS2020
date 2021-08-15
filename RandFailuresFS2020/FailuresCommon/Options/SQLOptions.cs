using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace FailuresCommon
{
    public class SQLOptions : SQLiteDataAccess
    {
        /// <summary>
        /// loads all records from Options table into list of OptionsModel
        /// </summary>
        /// <returns>
        /// List of OptionsModel
        /// </returns>
        public static List<OptionsModel> LoadOptions()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<OptionsModel>("select * from Options", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Updates row in table Options if row exists. If not then insserts this row
        /// </summary>
        /// <param name="option">
        /// OptionModel object to update in database
        /// </param>
        public static void UpdateOption(OptionsModel option)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                if (cnn.Execute("update Options set OptionValue = @OptionValue where OptionName = @OptionName", option) <= 0)
                {
                    cnn.Execute("insert into Options (OptionName, OptionValue) values (@OptionName, @OptionValue)", option);
                }
            }
        }

        /// <summary>
        /// Loads value of given option 
        /// </summary>
        /// <param name="optionName">
        /// name of option to take value from
        /// </param>
        /// <returns>
        /// "" if select returns no rows
        /// value of option if option exists
        /// </returns>
        public static string LoadOptionValue(string optionName)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var query = cnn.QueryFirst<string>(String.Format("select OptionValue from Options where OptionName = '{0}'", optionName));
                    return query;
                }
                catch
                {
                    return "";
                }
            }
        }
    }
}
