using System;
using System.Text.Json;

namespace SimpSettings 
{
    public class Setting<T> : ISetting
    {
        public event EventHandler<SettingValueChangedEventArgs<T>> OnValueChanged;
        
        public string Name { get; private set; }
        
        public T Default { get; private set; }

        public T Value
        {
            get => _value;
            set
            {
                OnValueChanged?.Invoke(this, new SettingValueChangedEventArgs<T>(_value, value));
                
                _value = value;
            }
        }
        
        private T _value;
        
        public Setting(string name, T defaultValue)
        {
            Name = name;
            Default = defaultValue;
            Value = defaultValue;
        }

        public void Reset() => Value = Default;

        public string Serialize()
        {
            return JsonSerializer.Serialize(Value);
        }
        
        public void Deserialize(string json)
        {
            Value = JsonSerializer.Deserialize<T>(json);
        }
    }

    internal interface ISetting
    {
        void Reset();

        string Serialize();
        void Deserialize(string json);
    }

    public class SettingValueChangedEventArgs<T> : EventArgs
    {
        public T OldValue { get; private set; }
        public T NewValue { get; private set; }
        
        public SettingValueChangedEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}