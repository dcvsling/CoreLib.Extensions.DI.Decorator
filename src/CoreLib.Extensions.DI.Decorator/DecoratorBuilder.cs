using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLib.Extensions.DependencyInjection.Decorator
{

    public class DecoratorBuilder
    {

        public DecoratorBuilder(IServiceCollection services, TypeInfo type)
        {
            Services = services;
            DecoratedType = type;
        }
        public IServiceCollection Services { get; }
        public TypeInfo DecoratedType { get; }
        public DecoratorBuilder DecorateBy<TImplement>()
            => DecorateBy(typeof(TImplement).GetTypeInfo());
        public DecoratorBuilder DecorateBy(TypeInfo implementType)
        {
            if(!DecoratedType.IsImplementFrom(implementType))
                throw new ArgumentException($"{implementType.GetType().FullName} is not implement {DecoratedType.FullName}");
            Services.AddSingleton<DecoratorType>(new DecoratorType(DecoratedType, implementType));
            return this;
        }
        
    }
}