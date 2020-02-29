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
        /// Gets the cached signature of the <see cref="AsyncActionInvocation"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> CreateAsyncActionInvocation { get; }
            = new Lazy<ConstructorInfo>(InitializeCreateAsyncActionInvocation, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     AsyncActionInvocation invocation;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="invocationVariable"> The emitted local <see cref="AsyncActionInvocation"/> variable. </param>
        public static void EmitLocalAsyncActionInvocationVariable(this ILGenerator body, out LocalBuilder invocationVariable)
        {
            invocationVariable = body.DeclareLocal(typeof(AsyncActionInvocation));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     invocation = new AsyncActionInvocation(parameter, methodSignature);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterVariable"> The local <see cref="Dictionary{TKey, TValue}"/> variable. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <param name="invocationVariable"> The local <see cref="AsyncActionInvocation"/> variable. </param>
        public static void EmitNewAsyncActionInvocation(
            this ILGenerator body,
            LocalBuilder parameterVariable,
            LocalBuilder methodSignatureVariable,
            LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldloc, parameterVariable.LocalIndex);
            body.Emit(OpCodes.Ldloc, methodSignatureVariable.LocalIndex);
            body.Emit(OpCodes.Newobj, CreateAsyncActionInvocation.Value);
            body.Emit(OpCodes.Stloc, invocationVariable.LocalIndex);
        }

        /// <summary>
        /// Initialization logic for the <see cref="CreateAsyncActionInvocation"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="AsyncActionInvocation"/> constructor. </returns>
        private static ConstructorInfo InitializeCreateAsyncActionInvocation()
        {
            var type = typeof(AsyncActionInvocation);
            var constructor = type.GetConstructor(new[]
                {
                    typeof(IDictionary<ParameterInfo, object?>),
                    typeof(MethodInfo)
                });
            return constructor ?? throw new ConstructorInfoException(type);
        }

        #endregion
    }
}