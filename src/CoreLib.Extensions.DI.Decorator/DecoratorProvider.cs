using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CoreLib.Extensions.DependencyInjection.Decorator
{
    internal class DecoratorProvider<T> : IDecoratorProvider<T> where T : class
    {
        private readonly IEnumerable<DecoratorType> _decoratorTypes;
        private readonly IServiceProvider _provider;

        public DecoratorProvider(IEnumerable<DecoratorType> decoratorTypes, IServiceProvider provider)
        {
            _decoratorTypes = decoratorTypes;
            _provider = provider;
        }
        public T Decorate(T target)
            => (T)_decoratorTypes
                .Where(x => typeof(T).IsAssignableFrom(
                    CloseGenericType(
                        typeof(T).GetTypeInfo(),
                        x.DecoratedType)))
                .Aggregate(
                    (object)target,
                    (seed, type) => (object)ActivatorUtilities.CreateInstance(
                        _provider, 
                        CloseGenericType(
                            typeof(T).GetTypeInfo(),
                            type.ImplementationType), 
                        seed));
        private Type CloseGenericType(TypeInfo decoratedType, TypeInfo implementType)
            => implementType.IsGenericTypeDefinition
                ? implementType.MakeGenericType(decoratedType.GetGenericArguments())
                : implementType;
    }
}
