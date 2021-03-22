using Microsoft.Extensions.DependencyInjection;

using System;

namespace CoreLib.Extensions.DependencyInjection.Decorator
{

    internal class Decorated<T> : IDecorated<T> where T : class
    {
        private readonly IDecoratorProvider<T> _decorator;
        private readonly IServiceProvider _provider;

        public Decorated(IDecoratorProvider<T> decorator, IServiceProvider provider)
        {
            _decorator = decorator;
            _provider = provider;
        }

        public T Value => _decorator.Decorate(_provider.GetRequiredService<T>());
    }
}
