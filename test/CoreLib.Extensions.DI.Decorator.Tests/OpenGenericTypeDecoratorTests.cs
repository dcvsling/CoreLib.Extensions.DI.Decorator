using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoreLib.Extensions.DependencyInjection.Decorator.Tests
{
    public class OpenGenericTypeDecoratorTest
    {
        [Fact]
        public void decorate_open_generic_type()
        {
            var services = new ServiceCollection()
                .AddSingleton<A>()
                .AddSingleton(typeof(IService<>), typeof(Service<>))
                .AddDecorator(typeof(IService<>).GetTypeInfo())
                    .DecorateBy(typeof(ServiceDecorator<>).GetTypeInfo())
                .Services;

            var service = services.BuildServiceProvider()
                .GetRequiredService<IDecorated<IService<A>>>()
                .Value;

            service.Invoke(default);
        }
    }
    public class A { }
    public interface IService<T>
    {
        void Invoke(T t);
    }
    public class Service<T> : IService<T>
    {

        public void Invoke(T t) => Assert.IsType<T>(t);
    }

    public class ServiceDecorator<T> : IService<T>
    {
        private readonly IService<T> service;
        private readonly T t;
        public ServiceDecorator(IService<T> service, T t)
        {
            this.t = t;
            this.service = service;
        }

        public void Invoke(T i)
            => service.Invoke(t);
    }
}