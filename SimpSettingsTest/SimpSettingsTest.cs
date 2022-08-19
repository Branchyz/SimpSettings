using System;

namespace SimpSettings.Test
{
    enum Season { Spring, Summer, Autumn, Winter }
    
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Season FavoriteSeason { get; set; }
        
        public override string ToString()
        {
            return $"{Name} is {Age} years old and likes {FavoriteSeason}";
        }
    }
    
    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
    
    public class SimpSettingsTest
    {
        public static void Main()
        {
            SettingsManager.AddSetting("test-int", 800);
            SettingsManager.AddSetting("test-float", 3.14f);
            SettingsManager.AddSetting("test-string", "Hello World");
            SettingsManager.AddSetting("test-bool", true);
            SettingsManager.AddSetting("test-date", new DateTime(2018, 1, 1));
            SettingsManager.AddSetting("test-enum", Season.Winter);
            SettingsManager.AddSetting("test-array", new[] { 1, 2, 3 });
            SettingsManager.AddSetting("test-class", new Person { Name = "John", Age = 30, FavoriteSeason = Season.Summer });
            SettingsManager.AddSetting("test-struct", new Point() { X = 1, Y = 2 });

            string modifiedSettingsJson =
                "{\n  \"test-int\": \"200\",\n  \"test-float\": \"28.3\",\n  \"test-string\": \"\\\"This string has changed!\\\"\",\n  \"test-bool\": \"false\",\n  \"test-date\": \"\\\"2006-02-01T00:00:00\\\"\",\n  \"test-enum\": \"2\",\n  \"test-array\": \"[10,20,30]\",\n  \"test-class\": \"{\\\"Name\\\":\\\"James\\\",\\\"Age\\\":56,\\\"FavoriteSeason\\\":3}\",\n  \"test-struct\": \"{\\\"X\\\":4,\\\"Y\\\":5}\"\n}";
            
            SettingsManager.Load(modifiedSettingsJson);
            
            Console.WriteLine("test-int: " + SettingsManager.GetValue<int>("test-int"));
            Console.WriteLine("test-float: " + SettingsManager.GetValue<float>("test-float"));
            Console.WriteLine("test-string: " + SettingsManager.GetValue<string>("test-string"));
            Console.WriteLine("test-bool: " + SettingsManager.GetValue<bool>("test-bool"));
            Console.WriteLine("test-date: " + SettingsManager.GetValue<DateTime>("test-date"));
            Console.WriteLine("test-enum: " + SettingsManager.GetValue<Season>("test-enum"));
            Console.WriteLine("test-array: " + string.Join(",", SettingsManager.GetValue<int[]>("test-array")));
            Console.WriteLine("test-class: " + SettingsManager.GetValue<Person>("test-class"));
            Console.WriteLine("test-struct: " + SettingsManager.GetValue<Point>("test-struct"));
        }
    }
}