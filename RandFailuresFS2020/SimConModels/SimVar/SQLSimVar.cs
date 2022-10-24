using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace SimConModels
{
    public class SQLSimVar : SQLiteDataAccess
    {
        public static List<SimVarModel> LoadFailableSimVarsList(int PresetID, bool OnlyEnabled = false, bool OnlyFailable = true)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string SQL = "SELECT * " +
                    "FROM           SimVar s, Presets p " +
                    "LEFT JOIN      VarsInPreset v " +
                    "   ON          v.simvarID = s.SimVarID " +
                    "   AND         v.PresetID = p.PresetID " +
                    $"WHERE         (p.PresetID = { PresetID } " +
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
                return output.ToList();
            }
        }

        public static List<SimVarModel> LoadDataList()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string SQL = "SELECT * FROM SimVar s WHERE s.IsFailable = 0 ";
                var output = cnn.Query<SimVarModel>(SQL, new DynamicParameters());
                return output.ToList();
            }
        }

        public static void Insert(List<SimVarModel> simVars, int presetID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                foreach (SimVarModel simVar in simVars)
                {
                    cnn.Execute($"insert into VarsInPreset (Enable, FailPercent, SimVarID, PresetID) values (@Enable, @FailPercent, @SimVarID, {presetID})", simVar);
                }
            }
        }

        public static void Delete(int presetID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"delete from VarsInPreset where PresetID = { presetID }");
            }
        }
    }
}
