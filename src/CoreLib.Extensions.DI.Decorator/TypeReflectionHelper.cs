using System.Linq;
using System.Reflection;

namespace CoreLib.Extensions.DependencyInjection.Decorator
{
    internal static class TypeReflectionHelper {
        public static bool IsImplementFrom(this TypeInfo decoratedType, TypeInfo implementType)
            => (decoratedType.IsGenericTypeDefinition 
                && implementType.IsGenericTypeDefinition
                && implementType.GetInterfaces()
                    .Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == decoratedType))
                || decoratedType.IsAssignableFrom(implementType);
    }
}