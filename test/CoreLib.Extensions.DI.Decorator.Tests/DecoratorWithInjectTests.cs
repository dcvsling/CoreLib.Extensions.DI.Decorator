using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoreLib.Extensions.DependencyInjection.Decorator.Tests
{
    public class DecoratorWithInjectTests
    {
        public class SimpleDecoratorTest
        {
            [Fact]
            public void decorate_with_inject_other_service_for_addone_service()
            {
                var services = new ServiceCollection()
                    .AddSingleton<IService, Service>()
                    .AddSingleton<Addin>()
                    .AddDecorator<IService>()
                    .DecorateBy<ServiceDecorator>()
                    .Services;

                var service = services.BuildServiceProvider()
                    .GetRequiredService<IDecorated<IService>>()
                    .Value;

                Assert.Equal(3, service.AddOne(1));
            }

        }
        public interface IService
        {
            int AddOne(int i);
        }
        public class Service : IService
        {
            public int AddOne(int i) => i + 1;
        }

        public class Addin
        {
            public int Value => 1;
        }

        public class ServiceDecorator : IService
        {
            private readonly IService service;
            private readonly Addin addin;
            public ServiceDecorator(IService service, Addin addin)
            {
                this.addin = addin;
                this.service = service;
            }

            public int AddOne(int i)
                => service.AddOne(i + addin.Value);
        }
    }
}