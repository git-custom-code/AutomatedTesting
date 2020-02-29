namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using ExceptionHandling;
    using Interception;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Extension methods for the <see cref="ILGenerator"/> type.
    /// </summary>
    public static partial class ILGeneratorExtensions
    {
        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="ActionInvocation"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> CreateActionInvocation { get; }
            = new Lazy<ConstructorInfo>(InitializeCreateActionInvocation, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     ActionInvocation invocation;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="invocationVariable"> The emitted local <see cref="ActionInvocation"/> variable. </param>
        public static void EmitLocalActionInvocationVariable(this ILGenerator body, out LocalBuilder invocationVariable)
        {
            invocationVariable = body.DeclareLocal(typeof(ActionInvocation));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     invocation = new ActionInvocation(parameter, methodSignature);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterVariable"> The local <see cref="Dictionary{TKey, TValue}"/> variable. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <param name="invocationVariable"> The local <see cref="ActionInvocation"/> variable. </param>
        public static void EmitNewActionInvocation(
            this ILGenerator body,
            LocalBuilder parameterVariable,
            LocalBuilder methodSignatureVariable,
            LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldloc, parameterVariable.LocalIndex);
            body.Emit(OpCodes.Ldloc, methodSignatureVariable.LocalIndex);
            body.Emit(OpCodes.Newobj, CreateActionInvocation.Value);
            body.Emit(OpCodes.Stloc, invocationVariable.LocalIndex);
        }

        /// <summary>
        /// Initialization logic for the <see cref="CreateActionInvocation"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="ActionInvocation"/> constructor. </returns>
        private static ConstructorInfo InitializeCreateActionInvocation()
        {
            var type = typeof(ActionInvocation);
            var constructor = type.GetConstructor(new[] { typeof(IDictionary<ParameterInfo, object>), typeof(MethodInfo) });
            return constructor ?? throw new ConstructorInfoException(type);
        }

        #endregion
    }
}