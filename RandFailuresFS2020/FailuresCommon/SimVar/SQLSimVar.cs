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
                    "FROM           SimVar s " +
                    "LEFT JOIN      TwitchVar t " +
                    "   ON          t.simvarID = s.SimVarID " +
                    "LEFT JOIN      Presets p " +
                    "   ON          t.presetID = p.PresetID " +
                    $"WHERE         p.PresetID = { PresetID } " +
                    $"OR            p.PresetID is null";
                var output = cnn.Query<SimVarModel>(SQL, new DynamicParameters());
                return output.ToList();
            }
        }

        /*public static void Insert(SimVarModel preset)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Presets (name) values (@name)", preset);
            }
        }

        public static void Delete(SimVarModel preset)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"delete from Presets where id = { preset.ID }");
            }
        }*/
    }
}
