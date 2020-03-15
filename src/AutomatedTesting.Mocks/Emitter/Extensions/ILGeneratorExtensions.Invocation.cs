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
        /// Gets the cached signature of the <see cref="Invocation"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> CreateInvocation { get; }
            = new Lazy<ConstructorInfo>(InitializeCreateInvocation, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="Array.Empty{T}"/> static method.
        /// </summary>
        private static Lazy<MethodInfo> ArrayEmptyInvocationFeature { get; }
            = new Lazy<MethodInfo>(InitializeArrayEmptyInvocationFeature, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits a local <see cref="Invocation"/> variable.
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="invocationVariable"> The emitted local <see cref="Invocation"/> variable. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     Invocation invocation;
        /// ]]>
        public static void EmitLocalInvocationVariable(this ILGenerator body, out LocalBuilder invocationVariable)
        {
            invocationVariable = body.DeclareLocal(typeof(Invocation));
        }

        /// <summary>
        /// Emits code to create a new <see cref="Invocation"/> instance for the given method signature and features.
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="invocationVariable"> The local <see cref="Invocation"/> variable. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <param name="invocationFeatureVariables"> A colleciton of local <see cref="IInvocationFeature"/> variables. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     invocation = new Invocation(methodSignature, feature1, feature2, ... featureN);
        /// ]]>
        /// </remarks>
        public static void EmitNewInvocation(
            this ILGenerator body,
            LocalBuilder invocationVariable,
            LocalBuilder methodSignatureVariable,
            IReadOnlyList<LocalBuilder> invocationFeatureVariables)
        {
            // methodSignature,
            body.Emit(OpCodes.Ldloc, methodSignatureVariable.LocalIndex);

            if (invocationFeatureVariables == null || invocationFeatureVariables.Count == 0)
            {
                // Array.Empty<IInvocationFeature>()
                body.Emit(OpCodes.Call, ArrayEmptyInvocationFeature.Value);
            }
            else
            {
                // feature1, feature2, ... featurenN
                body.Emit(OpCodes.Ldc_I4, invocationFeatureVariables.Count);
                body.Emit(OpCodes.Newarr, typeof(IInvocationFeature));

                for (var i = 0; i < invocationFeatureVariables.Count; ++i)
                {
                    var feature = invocationFeatureVariables[i];

                    body.Emit(OpCodes.Dup);
                    body.Emit(OpCodes.Ldc_I4, i);
                    body.Emit(OpCodes.Ldloc, feature.LocalIndex);
                    body.Emit(OpCodes.Stelem_Ref);
                }
            }

            // invocation = new Invocation(...)
            body.Emit(OpCodes.Newobj, CreateInvocation.Value);
            body.Emit(OpCodes.Stloc, invocationVariable.LocalIndex);
        }

        /// <summary>
        /// Initialization logic for the <see cref="CreateInvocation"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Invocation"/> constructor. </returns>
        private static ConstructorInfo InitializeCreateInvocation()
        {
            var type = typeof(Invocation);
            var constructor = type.GetConstructor(new[] { typeof(MethodInfo), typeof(IInvocationFeature[]) });
            return constructor ?? throw new ConstructorInfoException(type);
        }

        /// <summary>
        /// Initialization logic for the <see cref="ArrayEmptyInvocationFeature"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Array.Empty{T}"/> method. </returns>
        private static MethodInfo InitializeArrayEmptyInvocationFeature()
        {
            var type = typeof(Array);
            var methodName = nameof(Array.Empty);
            var openGenericArrayEmpty = @type.GetMethod(methodName, 1, Array.Empty<Type>());
            if (openGenericArrayEmpty == null)
            {
                throw new MethodInfoException(type, methodName);
            }
            var arrayEmpty = openGenericArrayEmpty.MakeGenericMethod(typeof(IInvocationFeature));
            return arrayEmpty ?? throw new MethodInfoException(type, methodName);
        }

        #endregion
    }
}