using System;
using System.Diagnostics;

namespace CustomCode.AutomatedTesting.Mocks.ExceptionHandling
{
    /// <summary>
    /// Syntactic sugar for input parameter validation.
    /// </summary>
    public static class Ensures
    {
        #region Logic

        /// <summary>
        /// Ensures that the given instance <paramref name="instance"/> is not null.
        /// </summary>
        /// <typeparam name="T"> The type of the given <paramref name="instance"/>. </typeparam>
        /// <param name="instance"> The instance to be validated. </param>
        /// <param name="instanceName">
        /// The name of the instance variable that is used in the <see cref="ArgumentNullException"/>
        /// as parameter name.
        /// </param>
        /// <exception cref="ArgumentNullException"> Thrown if <paramref name="instance"/> is null. </exception>
        [Conditional("DEBUG")]
        public static void NotNull<T>(T instance, string? instanceName = null)
            where T : class
        {
            if (instance == null)
            {
                if (instanceName != null)
                {
                    throw new ArgumentNullException(instanceName);
                }

                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Ensures that the given type <paramref name="type"/> is an interface.
        /// </summary>
        /// <param name="type"> The type to be validated. </param>
        /// <exception cref="ArgumentException"> Thrown if <paramref name="type"/> is not an interface. </exception>
        public static void IsInterface(Type type)
        {
            if (type.IsInterface == false)
            {
                throw new ArgumentException($"Invalid non-interface type '{type.FullName}'");
            }
        }

        /// <summary>
        /// Ensures that the given generic parameter <typeparamref name="T"/> is an interface.
        /// </summary>
        /// <typeparam name="T"> The type to be validated. </typeparam>
        /// <exception cref="ArgumentException"> Thrown if <typeparamref name="T"/> is not an interface. </exception>
        public static void IsInterface<T>()
        {
            if (typeof(T).IsInterface == false)
            {
                throw new ArgumentException($"Invalid non-interface type '{typeof(T).FullName}'");
            }
        }

        #endregion
    }
}
