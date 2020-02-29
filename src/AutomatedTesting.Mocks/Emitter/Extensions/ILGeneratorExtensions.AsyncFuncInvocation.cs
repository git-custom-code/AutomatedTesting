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
        /// Gets the cached signature of the <see cref="AsyncFuncInvocation"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> CreateAsyncFuncInvocation { get; }
            = new Lazy<ConstructorInfo>(InitializeCreateAsyncFuncInvocation, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     AsyncFuncInvocation invocation;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="invocationVariable"> The emitted local <see cref="AsyncFuncInvocation"/> variable. </param>
        public static void EmitLocalAsyncFuncInvocationVariable(this ILGenerator body, out LocalBuilder invocationVariable)
        {
            invocationVariable = body.DeclareLocal(typeof(AsyncFuncInvocation));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     invocation = new AsyncFuncInvocation(parameter, methodSignature);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterVariable"> The local <see cref="Dictionary{TKey, TValue}"/> variable. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <param name="invocationVariable"> The local <see cref="AsyncFuncInvocation"/> variable. </param>
        public static void EmitNewAsyncFuncInvocation(
            this ILGenerator body,
            LocalBuilder parameterVariable,
            LocalBuilder methodSignatureVariable,
            LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldloc, parameterVariable.LocalIndex);
            body.Emit(OpCodes.Ldloc, methodSignatureVariable.LocalIndex);
            body.Emit(OpCodes.Newobj, CreateAsyncFuncInvocation.Value);
            body.Emit(OpCodes.Stloc, invocationVariable.LocalIndex);
        }

        /// <summary>
        /// Initialization logic for the <see cref="CreateAsyncFuncInvocation"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="AsyncFuncInvocation"/> constructor. </returns>
        private static ConstructorInfo InitializeCreateAsyncFuncInvocation()
        {
            var type = typeof(AsyncFuncInvocation);
            var constructor = type.GetConstructor(new[] { typeof(IDictionary<ParameterInfo, object>), typeof(MethodInfo) });
            return constructor ?? throw new ConstructorInfoException(type);
        }

        #endregion
    }
}