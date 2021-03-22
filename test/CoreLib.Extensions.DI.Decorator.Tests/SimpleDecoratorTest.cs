using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoreLib.Extensions.DependencyInjection.Decorator.Tests
{
    public class SimpleDecoratorTest {
        [Fact]
        public void simple_decorate_addone_service   () {
            var services = new ServiceCollection()
                .AddSingleton<IService, Service>()
                .AddDecorator<IService>()
                .DecorateBy<ServiceDecorator>()
                .Services;

            var service = services.BuildServiceProvider()
                .GetRequiredService<IDecorated<IService>>()
                .Value;

            Assert.Equal(3, service.AddOne(1));
        }
    }
    public interface IService {
        int AddOne (int i);
    }
    public class Service : IService {
        public int AddOne (int i) => i + 1;
    }

    public class ServiceDecorator : IService {
        private readonly IService service;
        public ServiceDecorator (IService service) {
            this.service = service;
        }

        public int AddOne(int i) 
            => service.AddOne(i + 1);
    }
}