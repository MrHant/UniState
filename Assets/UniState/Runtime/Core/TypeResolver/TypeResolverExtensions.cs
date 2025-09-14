using System;
using System.Runtime.CompilerServices;

namespace UniState
{
    /// <summary>
    /// Extension methods for <see cref="ITypeResolver"/>.
    /// </summary>
    public static class TypeResolverExtensions
    {
        /// <summary>
        /// Resolves an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <param name="resolver">The type resolver.</param>
        /// <returns>An instance of the specified type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when resolver is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Resolve<T>(this ITypeResolver resolver)
        {
            if (resolver == null)
                throw new ArgumentNullException(nameof(resolver));
            
            return (T)resolver.Resolve(typeof(T));
        }
    }
}