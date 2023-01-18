using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Types.ObservableValues
{
    [Serializable]
    public class ObservableSecureInt : IObservableValue<Int>
    {
        
        public event Action<Int> Changed;
        private int _value;
        [JsonProperty] private int _serializableValue;

        [JsonIgnore]
        public Int Value
        {
            get => _value;
            set
            {
                Changed?.Invoke(value);
                _value = value;
            }
        }
        
        public ObservableSecureInt(Int value)
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

        public static implicit operator int(ObservableSecureInt secure) => secure.Value;
        public static implicit operator float(ObservableSecureInt secure) => secure.Value;

        public static implicit operator ObservableSecureInt(int value) => new ObservableSecureInt(value);

    }
}
