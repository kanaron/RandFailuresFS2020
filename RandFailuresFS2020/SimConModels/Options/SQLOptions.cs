using Dapper;
using Serilog;
using System.Data;
using System.Data.SQLite;

namespace SimConModels
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
            Log.Logger.Information("UpdateOption " + option.OptionName + " with value: " + option.OptionValue);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                if (cnn.Execute("update Options set OptionValue = @OptionValue where OptionName = @OptionName", option) <= 0)
                {
                    cnn.Execute("insert into Options (OptionName, OptionValue) values (@OptionName, @OptionValue)", option);
                }
            }
        }

        public static void UpdateOption(string optionName, string optionValue)
        {
            Log.Logger.Information("UpdateOption " + optionName + " with value: " + optionValue);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                if (cnn.Execute($"update Options set OptionValue = {optionValue} where OptionName = \"{optionName}\"") <= 0)
                {
                    cnn.Execute($"insert into Options (OptionName, OptionValue) values ({optionName}, {optionValue})");
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
        /// value of option if option exists
        /// </returns>
        public static string LoadOptionValue(string optionName)
        {
            Log.Logger.Information("LoadOptionValue " + optionName);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var query = cnn.QueryFirst<string>(String.Format("select OptionValue from Options where OptionName = '{0}'", optionName));
                    Log.Logger.Information("LoadOptionValue result: " + query);
                    return query;
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.StackTrace);
                    throw;
                }
            }
        }

        public static bool LoadOptionValueBool(string optionName)
        {
            Log.Logger.Information("LoadOptionValueBool " + optionName);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var query = cnn.QueryFirst<int>(String.Format("select OptionValue from Options where OptionName = '{0}'", optionName));
                    Log.Logger.Information("LoadOptionValueBool result: " + query);
                    return query > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.StackTrace);
                    throw;
                }
            }
        }

        public static int LoadOptionValueInt(string optionName)
        {
            Log.Logger.Information("LoadOptionValueInt " + optionName);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var query = cnn.QueryFirst<int>(String.Format("select OptionValue from Options where OptionName = '{0}'", optionName));
                    Log.Logger.Information("LoadOptionValueInt result: " + query);
                    return query;
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, ex.StackTrace);
                    throw;
                }
            }
        }
    }
}
