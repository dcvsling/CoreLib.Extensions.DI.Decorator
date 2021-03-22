using System;

namespace CoreLib.Extensions.DependencyInjection.Decorator
{
    public interface IDecorated<T> where T : class
    {
        T Value { get; }
    }
}
