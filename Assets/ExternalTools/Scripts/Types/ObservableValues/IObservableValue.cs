using System;

namespace ExternalTools.Scripts.Types.ObservableValues
{
    public interface IObservableValue<T>
    {
        event Action<T> Changed;

        T Value { get; set; }
    }
}