namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using ExceptionHandling;
    using Interception;
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
        /// Gets the cached signature of the <see cref="IInterceptor.Intercept(IInvocation)"/> method.
        /// </summary>
        private static Lazy<MethodInfo> Intercept { get; } = new Lazy<MethodInfo>(InitializeIntercept, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     _interceptor.Intercept(invocation);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method or property. </param>
        /// <param name="interceptorField"> The <see cref="IInterceptor"/> backing field. </param>
        /// <param name="invocationVariable"> The local <see cref="IInvocation"/> variable. </param>
        public static void EmitInterceptCall(
            this ILGenerator body, FieldBuilder interceptorField, LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldarg_0);
            body.Emit(OpCodes.Ldfld, interceptorField);
            body.Emit(OpCodes.Ldloc, invocationVariable.LocalIndex);
            body.Emit(OpCodes.Callvirt, Intercept.Value);
        }

        /// <summary>
        /// Initialization logic for the <see cref="Intercept"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="IInterceptor.Intercept(IInvocation)"/> method. </returns>
        private static MethodInfo InitializeIntercept()
        {
            var @type = typeof(IInterceptor);
            var methodName = nameof(IInterceptor.Intercept);
            var intercept = @type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            return intercept ?? throw new MethodInfoException(@type, methodName);
        }

        #endregion
    }
}