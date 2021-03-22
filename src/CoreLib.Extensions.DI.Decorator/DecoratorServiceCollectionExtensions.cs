using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CoreLib.Extensions.DependencyInjection.Decorator
{

    public static class DecoratorServiceCollectionExtensions
    {
        public static IServiceCollection AddDecorator(this IServiceCollection services)
        {
            services.TryAddTransient(typeof(IDecorated<>), typeof(Decorated<>));
            services.TryAddTransient(typeof(IDecoratorProvider<>), typeof(DecoratorProvider<>));
            services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(DecoratorType)));
            return services;
        }

        public static DecoratorBuilder AddDecorator<T>(this IServiceCollection services) where T : class
            => services.AddDecorator(typeof(T).GetTypeInfo());
        public static DecoratorBuilder AddDecorator(this IServiceCollection services, TypeInfo decoratedType)
            =>  new DecoratorBuilder(services.AddDecorator(), decoratedType);
    }
}