using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace FailuresCommon
{
    public class SQLSimVar : SQLiteDataAccess
    {
        //TODO - app config if twitch version and new table for joining simvar x preset with %
        public static List<SimVarModel> LoadSimVarsList(int PresetID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string SQL = "SELECT * " +
                    "FROM           SimVar s, Presets p " +
                    "LEFT JOIN      VarsInPreset v " +
                    "   ON          v.simvarID = s.SimVarID " +
                    "   AND         v.PresetID = p.PresetID " +
                    $"WHERE         (p.PresetID = { PresetID } " +
                    $"OR            p.PresetID is null) " +
                    $"AND           s.IsFailable = 1";
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
                    cnn.Execute($"insert into VarsInPreset (Enable, Price, FailPercent, SimVarID, PresetID) values (@Enable, @Price, @FailPercent, @SimVarID, {presetID})", simVar);
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
