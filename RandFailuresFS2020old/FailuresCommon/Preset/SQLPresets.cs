using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace FailuresCommon
{
    public class SQLPresets : SQLiteDataAccess
    {
        public static List<PresetModel> LoadPresets()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PresetModel>("select * from Presets", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void Insert(PresetModel preset)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Presets (PresetName) values (@PresetName)", preset);
            }
        }

        public static new void Insert(string presetName)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"insert into Presets (PresetName) values ('{presetName}')");
            }
        }

        public static void Delete(PresetModel preset)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"delete from Presets where PresetID = { preset.PresetID }");
            }
        }

        public static void Delete(int presetID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"delete from Presets where PresetID = { presetID }");
            }
        }
    }
}
