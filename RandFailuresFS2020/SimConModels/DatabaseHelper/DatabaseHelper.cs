using Dapper;
using Serilog;
using System.Data;
using System.Data.SQLite;
using System.Text.Json;

namespace SimConModels.DatabaseHelper
{
    public class DatabaseHelper : SQLiteDataAccess
    {
        private int jsonVersion = 0;
        private int databaseVersion = 0;
        private JsonModel jsonModel;
        private string jsonString;


        public DatabaseHelper()
        {
            CheckAndMoveDatabaseFile();
            CheckAndLoadJson();
            CheckAndLoadDatabase();

            if (jsonVersion > databaseVersion)
            {
                Log.Logger.Information("Updating database");
                UpdateDatabase();
                UpdateDatabaseVersion();
            }

            Log.Logger.Information("DatabaseHelper completed");
        }

        private void CheckAndLoadJson()
        {
            Log.Logger.Information("Checking json file");
            jsonString = File.ReadAllText("SimVarBase.json");
            Log.Logger.Information("Deserializing json file");
            jsonModel = JsonSerializer.Deserialize<JsonModel>(jsonString)!;
            jsonVersion = jsonModel.Version;
            Log.Logger.Information("Json version " + jsonVersion);
        }

        private void CheckAndLoadDatabase()
        {
            Log.Logger.Information("Checking database version");
            databaseVersion = SQLOptions.LoadOptionValueInt("DatabaseVersion");
        }

        private void CheckAndMoveDatabaseFile()
        {
            Log.Logger.Information("Checking if database file exists in database folder");
            if (!File.Exists(".\\database\\FailDB.db"))
            {
                Log.Logger.Information("Copying database file into database folder");
                File.Copy("FailDB.db", ".\\database\\FailDB.db");
            }
        }

        private void UpdateDatabase()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                Log.Logger.Information("Deleting SimVar table");
                cnn.Execute($"delete from SimVar");
                cnn.Execute($"delete from sqlite_sequence where name='SimVar'");

                foreach (var sim in jsonModel.SimVars)
                {
                    Log.Logger.Information("Inserting into SimVar " + sim.SimVarName);
                    cnn.Execute($"insert into " +
                        $"SimVar (SimVarID, SimVarName, SimVariable, Unit, Domain, IsEvent, IsLeak, IsStuck, IsComplete, IsFailable) " +
                        $"values (@SimVarID, @SimVarName, @SimVariable, @Unit, @Domain, {BoolToInt(sim.IsEvent)}, {BoolToInt(sim.IsLeak)}, " +
                        $"{BoolToInt(sim.IsStuck)}, {BoolToInt(sim.IsComplete)}, {BoolToInt(sim.IsFailable)})", sim);
                }
            }
        }

        private string BoolToInt(bool _b) => _b ? "1" : "0";

        private void UpdateDatabaseVersion()
        {
            Log.Logger.Information("Updating version of database");
            SQLOptions.UpdateOption("DatabaseVersion", jsonVersion.ToString());
        }
    }
}
