namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions
{
    using ExceptionHandling;
    using Interception.Parameters;
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Extension methods for the <see cref="ILGenerator"/> type.
    /// </summary>
    public static partial class ILGeneratorExtensions
    {
        #region Data

        /// <summary>
        /// Gets the cached signature of the parameter feature constructor.
        /// </summary>
        private static ConcurrentDictionary<Type, ConstructorInfo> ParameterFeatureCache { get; }
            = new ConcurrentDictionary<Type, ConstructorInfo>();

        /// <summary>
        /// Gets the cached signature of the <see cref="ParameterRef.GetValue{T}(string)"/> method.
        /// </summary>
        private static ConcurrentDictionary<Type, MethodInfo> ParameterRefGetValueCache { get; }
            = new ConcurrentDictionary<Type, MethodInfo>();

        #endregion

        #region Logic

        /// <summary>
        /// Emits a local parameter feature variable.
        /// </summary>
        /// <typeparam name="T">
        /// <see cref="ParameterIn"/>, <see cref="ParameterRef"/> or
        /// </typeparam>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterFeatureVariable"> The emitted local parameter variable. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     ParameterIn/Ref/Out parameterFeature;
        /// ]]>
        /// </remarks>
        public static void EmitLocalParameterFeatureVariable<T>(
            this ILGenerator body, out LocalBuilder parameterFeatureVariable)
        {
            parameterFeatureVariable = body.DeclareLocal(typeof(T));
        }

        /// <summary>
        /// Emits code to create a new parameter feature instance for the given feature.
        /// </summary>
        /// <typeparam name="T">
        /// <see cref="ParameterIn"/>, <see cref="ParameterRef"/> or
        /// </typeparam>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <param name="parameterSignatures"> The dynamic method's parameter signatures. </param>
        /// <param name="parameterFeatureVariable"> The emitted local parameter variable. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     parameterFeature = new ParameterIn/Ref/Out(methodSignature, new[] { parameter1, ... parameterN });
        /// ]]>
        /// </remarks>
        public static void EmitNewParameterFeature<T>(
            this ILGenerator body,
            LocalBuilder methodSignatureVariable,
            ParameterInfo[] parameterSignatures,
            LocalBuilder parameterFeatureVariable)
        {
            // methodSignature,
            body.Emit(OpCodes.Ldloc, methodSignatureVariable.LocalIndex);

            // new[] { parameter1, ... parameterN }
            body.Emit(OpCodes.Ldc_I4, parameterSignatures.Length);
            body.Emit(OpCodes.Newarr, typeof(object));
            for (var i=0; i< parameterSignatures.Length; ++i)
            {
                body.Emit(OpCodes.Dup);
                body.Emit(OpCodes.Ldc_I4, i);
                body.Emit(OpCodes.Ldarg, parameterSignatures[i].Position + 1);

                var type = parameterSignatures[i].ParameterType;
                if (type.IsByRef)
                {
                    type = type.GetElementType()
                        ?? throw new MethodInfoException(typeof(Type), nameof(Type.GetElementType));
                    body.Emit(OpCodes.Ldobj, type);
                }

                if (type.IsValueType)
                {
                    body.Emit(OpCodes.Box, type);
                }

                body.Emit(OpCodes.Stelem_Ref);
            }

            // parameterFeature = new ParameterIn/Ref/Out(...)
            var featureCtor = ParameterFeatureCache.GetOrAdd(typeof(T), _ => InitializeParameterFeature<T>());
            body.Emit(OpCodes.Newobj, featureCtor);
            body.Emit(OpCodes.Stloc, parameterFeatureVariable.LocalIndex);
        }

        /// <summary>
        /// Emit a call to <see cref="ParameterRef.GetValue{T}(string)"/> and store the result in the associated ref
        /// parameter of the intercepted method.
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="parameterSignature"> The signature of the ref parameter to be synced. </param>
        /// <param name="parameterRefFeatureVariable"> The emitted local parameter ref feature variable. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     refParameter = parameterRefFeature.GetValue<ParameterType>("ParameterName");
        /// ]]>
        /// </remarks>
        public static void EmitSyncRefParameter(
            this ILGenerator body,
            ParameterInfo parameterSignature,
            LocalBuilder parameterRefFeatureVariable)
        {
            body.Emit(OpCodes.Ldarg, parameterSignature.Position + 1);
            body.Emit(OpCodes.Ldloc, parameterRefFeatureVariable.LocalIndex);
            body.Emit(OpCodes.Ldstr, parameterSignature.Name ?? "unknown");

            var type = parameterSignature.ParameterType.GetElementType()
                ?? throw new MethodInfoException(typeof(Type), nameof(Type.GetElementType));
            var getValue = ParameterRefGetValueCache.GetOrAdd(type, _ => InitializeParameterRefGetValue(type));
            body.Emit(OpCodes.Callvirt, getValue);
            body.Emit(OpCodes.Stobj, type);
        }

        /// <summary>
        /// Initialization logic for a new <see cref="ParameterFeatureCache"/> item.
        /// </summary>
        /// <typeparam name="T">
        /// <see cref="ParameterIn"/>, <see cref="ParameterRef"/> or
        /// </typeparam>
        /// <returns> The signature of the feature constructor. </returns>
        private static ConstructorInfo InitializeParameterFeature<T>()
        {
            var type = typeof(T);
            var constructor = type.GetConstructor(new[] { typeof(MethodInfo), typeof(object?[]) });
            return constructor ?? throw new ConstructorInfoException(type);
        }

        /// <summary>
        /// Initialization logic for a new <see cref="ParameterRefGetValueCache"/> item.
        /// </summary>
        /// <param name="parameterType"> The type of the ref parameter. </param>
        /// <returns> The signature of the <see cref="ParameterRef.GetValue{T}(string)"/> method. </returns>
        private static MethodInfo InitializeParameterRefGetValue(Type parameterType)
        {
            var type = typeof(ParameterRef);
            var methodName = nameof(ParameterRef.GetValue);
            var getValue = type.GetMethod(methodName, new[] { typeof(string) });
            var getValueGeneric = getValue?.MakeGenericMethod(parameterType);
            return getValueGeneric ?? throw new MethodInfoException(type, methodName);
        }

        #endregion
    }
}