using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using Dapper;
using Serilog;

namespace SimConModels
{
    public class SQLSimVar : SQLiteDataAccess
    {
        public static List<SimVarModel> LoadFailableSimVarsList(int PresetID, bool OnlyEnabled = false, bool OnlyFailable = true)
        {
            Log.Logger.Information("LoadFailableSimVarsList");
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string SQL = "SELECT * " +
                    "FROM           SimVar s, Presets p " +
                    "LEFT JOIN      VarsInPreset v " +
                    "   ON          v.simvarID = s.SimVarID " +
                    "   AND         v.PresetID = p.PresetID " +
                    $"WHERE         (p.PresetID = {PresetID} " +
                    $"OR            p.PresetID is null) ";

                if (OnlyFailable)
                {
                    SQL += $"AND           s.IsFailable = 1 ";
                }
                else
                {
                    SQL += $"AND           s.IsFailable = 0 ";
                }

                if (OnlyEnabled)
                {
                    SQL += $"AND           v.Enable = 1";
                }
                var output = cnn.Query<SimVarModel>(SQL, new DynamicParameters());
                Log.Logger.Information("LoadFailableSimVarsList done");
                return output.ToList();
            }
        }

        public static List<SimVarModel> LoadDataList()
        {
            Log.Logger.Information("LoadDataList");
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string SQL = "SELECT * FROM SimVar s WHERE s.IsFailable = 0 ";
                var output = cnn.Query<SimVarModel>(SQL, new DynamicParameters());
                Log.Logger.Information("LoadDataList done");
                return output.ToList();
            }
        }

        public static void Insert(List<SimVarModel> simVars, int presetID)
        {
            Log.Logger.Information("SQLSimVar.Insert");
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (SimVarModel simVar in simVars)
                {
                    cnn.Execute($"insert into VarsInPreset (Enable, FailPercent, SimVarID, PresetID) values (@Enable, @FailPercent, @SimVarID, {presetID})", simVar);
                }
            }
            Log.Logger.Information("SQLSimVar.Insert done");
        }

        public static void UpdateAllPercentage(int presetID, int failPercent)
        {
            Log.Logger.Information("SQLSimVar.UpdateAllPercentage");
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"update VarsInPreset set FailPercent = {failPercent} where PresetID = {presetID}");
            }
            Log.Logger.Information("SQLSimVar.UpdateAllPercentage done");
        }

        public static void Delete(int presetID)
        {
            Log.Logger.Information("SQLSimVar.Delete");
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"delete from VarsInPreset where PresetID = {presetID}");
            }
            Log.Logger.Information("SQLSimVar.Delete done");
        }
    }
}
