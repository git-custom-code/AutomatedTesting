namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using ExceptionHandling;
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Extension methods for the <see cref="ILGenerator"/> type.
    /// </summary>
    public static partial class ILGeneratorExtensions
    {
        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="Type.GetTypeFromHandle(RuntimeTypeHandle)"/> method (^= typeof()).
        /// </summary>
        private static Lazy<MethodInfo> GetTypeFromHandle { get; } = new Lazy<MethodInfo>(InitializeGetTypeFromHandle, true);

        #endregion

        #region Logic

        /// <summary>
        /// Initialization logic for the <see cref="GetTypeFromHandle"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Type.GetTypeFromHandle(RuntimeTypeHandle)"/> method (^= typeof()).</returns>
        private static MethodInfo InitializeGetTypeFromHandle()
        {
            var @type = typeof(Type);
            var methodName = nameof(Type.GetTypeFromHandle);
            var getTypeFromHandle = @type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            return getTypeFromHandle ?? throw new MethodInfoException(type, methodName);
        }

        #endregion
    }
}