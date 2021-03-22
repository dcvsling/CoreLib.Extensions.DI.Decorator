using System;
using System.Reflection;

namespace CoreLib.Extensions.DependencyInjection.Decorator
{
    internal class DecoratorType
    {
        public TypeInfo DecoratedType { get; }
        public TypeInfo ImplementationType { get; }
        public DecoratorType(TypeInfo decoratedType, TypeInfo implementationType)
        {
            ImplementationType = implementationType;
            DecoratedType = decoratedType;
        }
    }
}