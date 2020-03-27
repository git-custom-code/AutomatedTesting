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
        /// Emits a call to the <see cref="IInterceptor.Intercept(IInvocation)"/> method.
        /// </summary>
        /// <param name="body"> The body of the dynamic method or property. </param>
        /// <param name="interceptorField"> The <see cref="IInterceptor"/> backing field. </param>
        /// <param name="invocationVariable"> The local <see cref="IInvocation"/> variable. </param>
        /// <remarks>
        /// Emits the following source code:
        ///
        /// <![CDATA[
        ///     _interceptor.Intercept(invocation);
        /// ]]>
        /// </remarks>
        public static void EmitInterceptCall(
            this ILGenerator body, FieldBuilder interceptorField, LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldarg_0);
            body.Emit(OpCodes.Ldfld, interceptorField);
            body.Emit(OpCodes.Ldloc, invocationVariable.LocalIndex);
            body.Emit(OpCodes.Callvirt, Intercept.Value);
            body.Emit(OpCodes.Pop);
        }

        /// <summary>
        /// Emits a call to the <see cref="IInterceptor.Intercept(IInvocation)"/> method along with an if statement.
        /// </summary>
        /// <param name="body"> The body of the dynamic method or property. </param>
        /// <param name="interceptorField"> The <see cref="IInterceptor"/> backing field. </param>
        /// <param name="invocationVariable"> The local <see cref="IInvocation"/> variable. </param>
        /// <param name="elseLabel"> The label that defines the start of the "else" block. </param>
        /// <remarks>
        /// Emits the following source code:
        /// 
        /// <![CDATA[
        ///     if(_interceptor.Intercept(invocation))
        /// ]]>
        /// </remarks>
        public static void EmitIfInterceptCall(
            this ILGenerator body,
            FieldBuilder interceptorField,
            LocalBuilder invocationVariable,
            out Label elseLabel)
        {
            body.Emit(OpCodes.Ldarg_0);
            body.Emit(OpCodes.Ldfld, interceptorField);
            body.Emit(OpCodes.Ldloc, invocationVariable.LocalIndex);
            body.Emit(OpCodes.Callvirt, Intercept.Value);
            elseLabel = body.DefineLabel();
            body.Emit(OpCodes.Brtrue_S, elseLabel);
        }

        /// <summary>
        /// Emits an IL label for the matching <see cref="EmitIfInterceptCall(ILGenerator, FieldBuilder, LocalBuilder, out Label)"/>
        /// call (i.e. the begin of the "else" block)
        /// </summary>
        /// <param name="body"> The body of the dynamic method or property. </param>
        /// <param name="elseLabel"> The label that defines the start of the "else" block. </param>
        public static void EmitElseInterceptCall(this ILGenerator body, Label elseLabel)
        {
            body.MarkLabel(elseLabel);
        }

        /// <summary>
        /// Initialization logic for the <see cref="Intercept"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="IInterceptor.Intercept(IInvocation)"/> method. </returns>
        private static MethodInfo InitializeIntercept()
        {
            var type = typeof(IInterceptor);
            var methodName = nameof(IInterceptor.Intercept);
            var intercept = @type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            return intercept ?? throw new MethodInfoException(type, methodName);
        }

        #endregion
    }
}