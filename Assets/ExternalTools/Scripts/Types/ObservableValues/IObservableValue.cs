using System;

namespace Types.ObservableValues
{
    public interface IObservableValue<T>
    {
        event Action<T> Changed;

        T Value { get; set; }
    }
}