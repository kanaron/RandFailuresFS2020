using Dapper;
using Serilog;
using System.Data;
using System.Data.SQLite;

namespace SimConModels
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

        public static PresetModel LoadPreset(int presetID)
        {
            Log.Logger.Information("SQLPreset.LoadPreset " + presetID);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.QueryFirst<PresetModel>($"select * from Presets where PresetID = {presetID}", new DynamicParameters());
                return output;
            }
        }

        public static void UpdatePreset(PresetModel preset)
        {
            Log.Logger.Information("SQLPreset.UpdatePreset " + preset.PresetName);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                if (cnn.Execute("update Presets set PresetName = @PresetName, SpeedEnabled = @SpeedEnabled, SpeedMin = @SpeedMin, SpeedMax = @SpeedMax," +
                    "AltEnabled = @AltEnabled, AltMin = @AltMin, AltMax = @AltMax, TimeEnabled = @TimeEnabled," +
                    "TimeMin = @TimeMin, TimeMax = @TimeMax, InstantEnabled = @InstantEnabled where PresetID = @PresetID", preset) <= 0)
                {
                    cnn.Execute("insert into Presets (PresetName, SpeedEnabled, SpeedMin, SpeedMax, ) values (@OptionName, @OptionValue)", preset);
                }
            }
        }

        public static void Insert(PresetModel preset)
        {
            Log.Logger.Information("SQLPreset.Insert " + preset.PresetName);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Presets (PresetName) values (@PresetName)", preset);
            }
        }

        public static new void Insert(string presetName)
        {
            Log.Logger.Information("SQLPreset.Insert " + presetName);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"insert into Presets (PresetName) values ('{presetName}')");
            }
        }

        public static void Delete(PresetModel preset)
        {
            Log.Logger.Information("SQLPreset.Delete " + preset.PresetName);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"delete from Presets where PresetID = {preset.PresetID}");
            }
        }

        public static void Delete(int presetID)
        {
            Log.Logger.Information("SQLPreset.Delete " + presetID);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute($"delete from Presets where PresetID = {presetID}");
            }
        }
    }
}
