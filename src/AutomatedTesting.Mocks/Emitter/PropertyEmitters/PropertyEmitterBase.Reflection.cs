namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System;
    using System.Reflection;

    /// <summary>
    /// Abstract base type for <see cref="IPropertyEmitter"/> interface implementations that defines
    /// a set of common functionality that can be used by the specialized implementations.
    /// </summary>
    public abstract partial class PropertyEmitterBase
    {
        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="Type.GetProperty(string)"/> method.
        /// </summary>
        protected static Lazy<MethodInfo> GetProperty { get; } = new Lazy<MethodInfo>(InitializeGetProperty, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="System.Type.GetTypeFromHandle(RuntimeTypeHandle)"/> method (^= typeof()).
        /// </summary>
        protected static Lazy<MethodInfo> GetTypeFromHandle { get; } = new Lazy<MethodInfo>(InitializeGetTypeFromHandle, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="IInterceptor.Intercept(IInvocation)"/> method.
        /// </summary>
        protected static Lazy<MethodInfo> Intercept { get; } = new Lazy<MethodInfo>(InitializeIntercept, true);

        #endregion

        #region Logic

        /// <summary>
        /// Initialization logic for the <see cref="GetProperty"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Type.GetProperty(string)"/> method. </returns>
        private static MethodInfo InitializeGetProperty()
        {
            var getProperty = typeof(Type).GetMethod(
                nameof(System.Type.GetProperty),
                new[] { typeof(string) });
            return getProperty ?? throw new NullReferenceException(nameof(GetProperty));
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetTypeFromHandle"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="System.Type.GetTypeFromHandle(RuntimeTypeHandle)"/> method (^= typeof()).</returns>
        private static MethodInfo InitializeGetTypeFromHandle()
        {
            var getTypeFromHandle = typeof(Type).GetMethod(
                nameof(System.Type.GetTypeFromHandle),
                BindingFlags.Static | BindingFlags.Public);
            return getTypeFromHandle ?? throw new ArgumentNullException(nameof(GetTypeFromHandle));
        }

        /// <summary>
        /// Initialization logic for the <see cref="Intercept"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="IInterceptor.Intercept(IInvocation)"/> method. </returns>
        private static MethodInfo InitializeIntercept()
        {
            var intercept = typeof(IInterceptor).GetMethod(
                nameof(IInterceptor.Intercept),
                BindingFlags.Public | BindingFlags.Instance);
            return intercept ?? throw new ArgumentNullException(nameof(Intercept));
        }

        #endregion
    }
}