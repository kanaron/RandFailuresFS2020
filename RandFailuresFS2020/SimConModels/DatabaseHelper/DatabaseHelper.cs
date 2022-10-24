using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Data;
using System.Data.SQLite;
using Dapper;

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
                UpdateDatabase();
                UpdateDatabaseVersion();
            }
        }

        private void CheckAndLoadJson()
        {
            jsonString = File.ReadAllText("SimVarBase.json");
            jsonModel = JsonSerializer.Deserialize<JsonModel>(jsonString)!;
            jsonVersion = jsonModel.Version;
        }

        private void CheckAndLoadDatabase()
        {
            databaseVersion = SQLOptions.LoadOptionValueInt("DatabaseVersion");
        }

        private void CheckAndMoveDatabaseFile()
        {
            if (!File.Exists(".\\database\\FailDB.db"))
                File.Copy("FailDB.db", ".\\database\\FailDB.db");
        }

        private void UpdateDatabase()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"delete from SimVar");
                cnn.Execute($"delete from sqlite_sequence where name='SimVar'");

                foreach (var sim in jsonModel.SimVars)
                {
                    cnn.Execute($"insert into " +
                        $"SimVar (SimVarID, SimVarName, SimVariable, Unit, Domain, IsEvent, IsLeak, IsStuck, IsComplete, IsFailable) " +
                        $"values (@SimVarID, @SimVarName, @SimVariable, @Unit, @Domain, {BoolToInt(sim.IsEvent)}, {BoolToInt(sim.IsLeak)}, " +
                        $"{BoolToInt(sim.IsStuck)}, {BoolToInt(sim.IsCompleteFail)}, {BoolToInt(sim.IsFailable)})", sim);
                }
            }
        }

        private string BoolToInt(bool _b) => _b ? "1" : "0";

        private void UpdateDatabaseVersion()
        {
            SQLOptions.UpdateOption("DatabaseVersion", jsonVersion.ToString());
        }
    }
}
