using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Types.ObservableValues
{
    [Serializable]
    public class ObservableBool : IObservableValue<bool>
    {
        public event Action<bool> Changed;
        private bool _value;
        [JsonProperty] private bool _serializableValue;

        [JsonIgnore]
        public bool Value
        {
            get => _value;
            set
            {
                Changed?.Invoke(value);
                _value = value;
            }
        }

        public ObservableBool(bool value)
        {
            Value = value;
        }

        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            _serializableValue = Value;
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            Value = _serializableValue;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public static implicit operator bool(ObservableBool observableBool) => observableBool.Value;

    }
}