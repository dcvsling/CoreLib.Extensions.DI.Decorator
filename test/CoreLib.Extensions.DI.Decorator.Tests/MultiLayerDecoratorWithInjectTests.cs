using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoreLib.Extensions.DependencyInjection.Decorator.Tests
{
    public class MultiLayerDecoratorWithInjectTests
    {
        public class SimpleDecoratorTest
        {
            [Fact]
            public void multi_layer_decorate_with_inject_other_service_for_addone_service()
            {
                var services = new ServiceCollection()
                    .AddSingleton<IService, Service>()
                    .AddSingleton<PlusTwoAddin>()
                    .AddSingleton<DoubleAddin>()
                    .AddDecorator<IService>()
                        .DecorateBy<ServiceDecoratorFirst>()
                        .DecorateBy<ServiceDecoratorSecond>()
                        .DecorateBy<ServiceDecoratorThird>()
                        .DecorateBy<ServiceDecoratorSecond>()
                        .DecorateBy<ServiceDecoratorThird>()
                    .Services;

                var service = services.BuildServiceProvider()
                    .GetRequiredService<IDecorated<IService>>()
                    .Value;
                var expect = ((((1 * 2) + 2) * 2) + 2) + 1 + 1;
                Assert.Equal(expect, service.AddOne(1));
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

        public class PlusTwoAddin
        {
            public int AddTwo(int i) => i + 2;
        }
        public class DoubleAddin
        {
            public int DoubleIt(int i) => i * 2;
        }
        public class ServiceDecoratorFirst : IService
        {
            private readonly IService service;
            public ServiceDecoratorFirst(IService service)
            {
                this.service = service;
            }

            public int AddOne(int i)
                => service.AddOne(i + 1);
        }

        public class ServiceDecoratorSecond : IService
        {
            private readonly IService service;
            private readonly PlusTwoAddin addin;
            public ServiceDecoratorSecond(IService service, PlusTwoAddin addin)
            {
                this.addin = addin;
                this.service = service;
            }

            public int AddOne(int i)
                => service.AddOne(addin.AddTwo(i));
        }

        public class ServiceDecoratorThird : IService
        {
            private readonly IService service;
            private readonly DoubleAddin addin;
            public ServiceDecoratorThird(IService service, DoubleAddin addin)
            {
                this.addin = addin;
                this.service = service;
            }

            public int AddOne(int i)
                => service.AddOne(addin.DoubleIt(i));
        }
    }
}