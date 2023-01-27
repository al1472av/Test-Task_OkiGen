using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Random = System.Random;

namespace ExternalTools.Scripts.Types
{
    interface ISecurable
    {
        void Clear();
    }
    
    interface ISecurable<T, T2> : ISecurable, IComparable<T2>, IComparer<T2>, IEqualityComparer<T2>
    {
        T Value { get; }
    }

    [Serializable]
    public struct Int : ISecurable<int, Int>
    {
        private int _offset;
        private int _value;
        [JsonProperty] private int _serializableValue;
        
        public Int(int value)
        {
            _offset = new Random().Next(-10000, 10000);
            _value = value + _offset;
            _serializableValue = 0;
        }
        
        [OnSerialized]
        public void OnSerializing(StreamingContext context)
        {
            _serializableValue = Value;
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            Value = _serializableValue;
        }

        public int Value
        {
            get => _value - _offset;
            set
            {
                _offset = new Random().Next(-10000, 10000);
                _value = value + _offset;
            }
        }

        public void Clear() => _value = _offset = 0;
        public int Compare(Int x, Int y) => x.Value > y.Value ? 1 : x.Value < y.Value ? -1 : 0;
        public int CompareTo(Int other) => Value > other.Value ? 1 : Value < other.Value ? -1 : 0;
        public bool Equals(Int x, Int y) => x.GetHashCode() == y.GetHashCode();
        public int GetHashCode(Int obj) => obj.Value.GetHashCode();

        public override string ToString() => Value.ToString();
        public override bool Equals(object obj) => GetHashCode() == obj.GetHashCode();
        public override int GetHashCode() => Value.GetHashCode();

        public static Int operator +(Int x, Int y) => new Int(x.Value + y.Value);
        public static Int operator -(Int x, Int y) => new Int(x.Value - y.Value);
        public static Int operator *(Int x, Int y) => new Int(x.Value * y.Value);
        public static Int operator /(Int x, Int y) => new Int(x.Value / y.Value);
        public static Int operator ++(Int x) => new Int(x.Value + 1);
        public static Int operator --(Int x) => new Int(x.Value - 1);

        public static implicit operator Int(int value) => new Int(value);
        public static implicit operator int(Int secure) => secure.Value;

        public static explicit operator uint(Int secure) => (uint)secure.Value;
        public static explicit operator float(Int secure) => secure.Value;

        public static bool operator >(Int x, Int y) => x.Value > y.Value;
        public static bool operator <(Int x, Int y) => x.Value < y.Value;
        public static bool operator >=(Int x, Int y) => x.Value >= y.Value;
        public static bool operator <=(Int x, Int y) => x.Value <= y.Value;
        public static bool operator !=(Int x, Int y) => x.Value != y.Value;
        public static bool operator ==(Int x, Int y) => x.Value == y.Value;
        public static bool operator >(Int x, int y) => x.Value > y;
        public static bool operator <(Int x, int y) => x.Value < y;
        public static bool operator >=(Int x, int y) => x.Value >= y;
        public static bool operator <=(Int x, int y) => x.Value <= y;
        public static bool operator !=(Int x, int y) => x.Value != y;
        public static bool operator ==(Int x, int y) => x.Value == y;
        public static bool operator >(Int x, uint y) => x.Value > y;
        public static bool operator <(Int x, uint y) => x.Value < y;
        public static bool operator >=(Int x, uint y) => x.Value >= y;
        public static bool operator <=(Int x, uint y) => x.Value <= y;
        public static bool operator !=(Int x, uint y) => x.Value != y;
        public static bool operator ==(Int x, uint y) => x.Value == y;
    }
    
}