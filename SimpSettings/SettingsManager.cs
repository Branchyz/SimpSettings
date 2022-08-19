using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SimpSettings
{
    public static class SettingsManager
    {
        private static readonly Dictionary<string, ISetting> Settings = new();

        public static void AddSetting<T>(string name, T defaultValue)
        {
            AddSetting(new Setting<T>(name, defaultValue));    
        }
        
        public static void AddSetting<T>(Setting<T> setting)
        {
            Settings.Add(setting.Name, setting);
        }
        
        public static void RemoveSetting(string name)
        {
            Settings.Remove(name);
        }
        
        public static void RemoveSetting<T>(Setting<T> setting)
        {
            Settings.Remove(setting.Name);
        }

        public static bool TryGetSetting<T>(string name, out Setting<T> setting)
        {
            if (Settings.TryGetValue(name, out var s))
            {
                try
                {
                    setting = (Setting<T>)s;
                }
                catch (InvalidCastException)
                {
                    setting = null;
                    return false;
                }
                
                return true;
            }
            setting = null;
            return false;
        }
        
        public static T GetValue<T> (string name)
        {
            if (Settings.TryGetValue(name, out var s))
            {
                return ((Setting<T>)s).Value;
            }
            return default;
        }

        public static string ToJson()
        {
            Dictionary<string, string> data = new();

            foreach(var entry in Settings)
            {
                data.Add(entry.Key, entry.Value.Serialize());
            }

            return JsonSerializer.Serialize(data);
        }

        public static void Load(string json)
        {
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            if (data == null) return;

            foreach (var entry in data)
            {
                Settings[entry.Key].Deserialize(entry.Value);
            }
        }
    }
}
