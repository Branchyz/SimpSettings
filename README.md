# Simp(lefied) Settings
### An easy to use Settings Manager build using C#

## API
### Creating and removing a setting:
```
Setting<int> setting = new Setting<int>("Sensitivity", 90);

SettingsManager.AddSetting("Sensitivity", 90);
SettingsManager.AddSetting(setting);

SettingsManager.RemoveSetting("Sensitivity");
SettingsManager.RemoveSetting(setting);
```
It doesn't matter what overload you use.

### Getting the setting:
```
SettingsManager.TryGetSetting("Sensitivity", out Setting<int> sensitivitySetting);

sensitivity.Value = 80;
Console.WriteLine(sensitivity.Default);
sensitivity.Reset();
```

You can also get the value more easily:
```
Console.WriteLine(SettingsManager.GetValue<int>("Sensitivity"));
```

### Subscribing to the OnValueChanged Event:
```
SettingsManager.TryGetSetting("Sensitivity", out Setting<int> sensitivitySetting);
sensitivitySetting.OnValueChanged += SensitivityChanged
```

### Saving and loading by JSON:
```
File.WriteAllText("settings.json", SettingsManager.ToJson());

SettingsManager.Load(File.ReadAllText("settings.json"));
```

### You can use any object with a setting. It doesn't matter what the Type is.
