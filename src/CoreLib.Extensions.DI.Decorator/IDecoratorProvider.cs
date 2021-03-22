namespace CoreLib.Extensions.DependencyInjection.Decorator
{
    public interface IDecoratorProvider<T> where T : class
    {
        T Decorate(T target);
    }
}