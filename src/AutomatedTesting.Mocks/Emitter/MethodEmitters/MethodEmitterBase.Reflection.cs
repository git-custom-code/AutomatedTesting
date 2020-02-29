namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Abstract base type for <see cref="IMethodEmitter"/> interface implementations that defines
    /// a set of common functionality that can be used by the specialized implementations.
    /// </summary>
    public abstract partial class MethodEmitterBase
    {
        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="Dictionary{TKey, TValue}.Add(TKey, TValue)"/> method.
        /// </summary>
        protected static Lazy<MethodInfo> Add { get; } = new Lazy<MethodInfo>(InitialzeAdd, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="Dictionary{TKey, TValue}"/> constructor.
        /// </summary>
        protected static Lazy<ConstructorInfo> DictionaryConstructor { get; } = new Lazy<ConstructorInfo>(InitializeDictionaryConstructor, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="Type.GetMethod(string, System.Type[])"/> method.
        /// </summary>
        protected static Lazy<MethodInfo> GetMethod { get; } = new Lazy<MethodInfo>(InitializeGetMethod, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="MethodBase.GetParameters()"/> method.
        /// </summary>
        protected static Lazy<MethodInfo> GetParameters { get; } = new Lazy<MethodInfo>(InitializeGetParameters, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="System.Type.GetTypeFromHandle(RuntimeTypeHandle)"/> method (^= typeof()).
        /// </summary>
        protected static Lazy<MethodInfo> GetTypeFromHandle { get; } = new Lazy<MethodInfo>(InitializeGetTypeFromHandle, true);

        #endregion

        #region Logic

        /// <summary>
        /// Initialization logic for the <see cref="Add"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Dictionary{TKey, TValue}.Add(TKey, TValue)"/> method. </returns>
        private static MethodInfo InitialzeAdd()
        {
            var add = typeof(Dictionary<string, object>).GetMethod(nameof(Dictionary<ParameterInfo, object>.Add));
            return add ?? throw new ArgumentNullException(nameof(Add));
        }

        /// <summary>
        /// Initialization logic for the <see cref="DictionaryConstructor"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Dictionary{TKey, TValue}"/> constructor. </returns>
        private static ConstructorInfo InitializeDictionaryConstructor()
        {
            var constructor = typeof(Dictionary<ParameterInfo, object>).GetConstructor(Array.Empty<Type>());
            return constructor ?? throw new ArgumentNullException(nameof(DictionaryConstructor));
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetMethod"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Type.GetMethod(string, System.Type[])"/> method. </returns>
        private static MethodInfo InitializeGetMethod()
        {
            var getMethod = typeof(Type).GetMethod(
                nameof(System.Type.GetMethod),
                new[] { typeof(string), typeof(Type[]) });
            return getMethod ?? throw new ArgumentNullException(nameof(GetMethod));
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetParameters"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="MethodBase.GetParameters()"/> method. </returns>
        private static MethodInfo InitializeGetParameters()
        {
            var getParameters = typeof(MethodInfo).GetMethod(nameof(MethodInfo.GetParameters), Array.Empty<Type>());
            return getParameters ?? throw new ArgumentNullException(nameof(GetParameters));
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetParameters"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="System.Type.GetTypeFromHandle(RuntimeTypeHandle)"/> method (^= typeof()).</returns>
        private static MethodInfo InitializeGetTypeFromHandle()
        {
            var getTypeFromHandle = typeof(Type).GetMethod(
                nameof(System.Type.GetTypeFromHandle),
                BindingFlags.Static | BindingFlags.Public);
            return getTypeFromHandle ?? throw new ArgumentNullException(nameof(GetTypeFromHandle));
        }

        #endregion
    }
}