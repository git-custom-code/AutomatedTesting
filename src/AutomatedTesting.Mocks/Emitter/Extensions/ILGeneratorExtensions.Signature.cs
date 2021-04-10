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
        /// Gets the cached signature of the <see cref="Type.GetMethod(string, Type[])"/> method.
        /// </summary>
        private static Lazy<MethodInfo> GetMethod { get; } = new Lazy<MethodInfo>(InitializeGetMethod, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="Type.GetProperty(string)"/> method.
        /// </summary>
        private static Lazy<MethodInfo> GetProperty { get; } = new Lazy<MethodInfo>(InitializeGetProperty, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="Array.Empty{T}"/> static method.
        /// </summary>
        private static Lazy<MethodInfo> ArrayEmptyType { get; }
            = new Lazy<MethodInfo>(InitializeArrayEmptyType, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="Type.MakeByRefType"/> method.
        /// </summary>
        private static Lazy<MethodInfo> MakeByRefType { get; }
            = new Lazy<MethodInfo>(InitializeMakeByRefType, true);

        #endregion

        #region Logic

        /// <summary>
        /// Emits a local <see cref="MethodInfo"/> variable.
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     MethodInfo methodSignature;
        /// ]]>
        /// </remarks>
        public static void EmitLocalMethodSignatureVariable(this ILGenerator body, out LocalBuilder methodSignatureVariable)
        {
            methodSignatureVariable = body.DeclareLocal(typeof(MethodInfo));
        }

        /// <summary>
        /// Emits a local <see cref="PropertyInfo"/> variable.
        /// </summary>
        /// <param name="body"> The body of the dynamic property. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        /// <remarks>
        /// /// Emits the following source code:
        /// <![CDATA[
        ///     PropertyInfo propertySignature;
        /// ]]>
        /// </remarks>
        public static void EmitLocalPropertySignatureVariable(this ILGenerator body, out LocalBuilder propertySignatureVariable)
        {
            propertySignatureVariable = body.DeclareLocal(typeof(PropertyInfo));
        }

        /// <summary>
        /// Emits a call to <see cref="Type.GetMethod(string, Type[])"/> in order to get the signature of the invoked method.
        /// </summary>
        /// <param name="body"> The body of the dynamic method. </param>
        /// <param name="signature"> The dynamic method's signature. </param>
        /// <param name="methodSignatureVariable"> The emitted local <see cref="MethodInfo"/> variable. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     methodSignature = typeof(Interface).GetMethod(nameof(Method), new[] { typeof(parameter1), ... typeof(parameterN) });
        /// ]]>
        /// </remarks>
        public static void EmitGetMethodSignature(
            this ILGenerator body,
            MethodInfo signature,
            LocalBuilder methodSignatureVariable)
        {
            Ensures.NotNull(signature, nameof(signature));
            Ensures.NotNull(methodSignatureVariable, nameof(methodSignatureVariable));

            // nameof(Method)
#nullable disable
            body.Emit(OpCodes.Ldtoken, signature.DeclaringType);
#nullable restore
            body.Emit(OpCodes.Call, GetTypeFromHandle.Value);
            body.Emit(OpCodes.Ldstr, signature.Name);

            var parameters = signature.GetParameters();
            if (parameters.Length == 0)
            {
                // Array.Empty<Type>()
                body.Emit(OpCodes.Call, ArrayEmptyType.Value);
            }
            else
            {
                // new[] { typeof(parmeter1), ... typeof(parameterN) }
                body.Emit(OpCodes.Ldc_I4, parameters.Length);
                body.Emit(OpCodes.Newarr, typeof(Type));

                for (var i = 0u; i < parameters.Length; ++i)
                {
                    var parameter = parameters[i];

                    body.Emit(OpCodes.Dup);
                    body.Emit(OpCodes.Ldc_I4, i);

                    var type = parameter.ParameterType;
                    if (parameter.ParameterType.IsByRef)
                    {
                        type = type.GetElementType() ?? throw new MethodInfoException(typeof(Type), nameof(Type.GetElementType));
                    }

                    body.Emit(OpCodes.Ldtoken, type);
                    body.Emit(OpCodes.Call, GetTypeFromHandle.Value);

                    if (parameter.ParameterType.IsByRef)
                    {
                        body.Emit(OpCodes.Callvirt, MakeByRefType.Value);
                    }

                    body.Emit(OpCodes.Stelem_Ref);
                }
            }

            // methodSignature = GetMethod
            body.Emit(OpCodes.Call, GetMethod.Value);
            body.Emit(OpCodes.Stloc, methodSignatureVariable.LocalIndex);
        }

        /// <summary>
        /// Emits a call to <see cref="Type.GetProperty(string)"/> in order to get the signature of the invoked property.
        /// </summary>
        /// <param name="body"> The body of the dynamic property. </param>
        /// <param name="signature"> The dynamic property's signature. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        /// <remarks>
        /// Emits the following source code:
        /// <![CDATA[
        ///     propertySignature = typeof(Interface).GetProperty(nameof(Property), new[] { typeof(parameter1), ... typeof(parameterN) });
        /// ]]>
        /// </remarks>
        public static void EmitGetPropertySignature(
            this ILGenerator body,
            PropertyInfo signature,
            LocalBuilder propertySignatureVariable)
        {
            Ensures.NotNull(signature, nameof(signature));
            Ensures.NotNull(propertySignatureVariable, nameof(propertySignatureVariable));

            // nameof(Property)
#nullable disable
            body.Emit(OpCodes.Ldtoken, signature.DeclaringType);
#nullable restore
            body.Emit(OpCodes.Call, GetTypeFromHandle.Value);
            body.Emit(OpCodes.Ldstr, signature.Name);

            var parameters = signature.GetIndexParameters();
            if (parameters.Length == 0)
            {
                // Array.Empty<Type>()
                body.Emit(OpCodes.Call, ArrayEmptyType.Value);
            }
            else
            {
                // new[] { typeof(parmeter1), ... typeof(parameterN) }
                body.Emit(OpCodes.Ldc_I4, parameters.Length);
                body.Emit(OpCodes.Newarr, typeof(Type));
                for (var i = 0u; i < parameters.Length; ++i)
                {
                    body.Emit(OpCodes.Dup);
                    body.Emit(OpCodes.Ldc_I4, i);
                    body.Emit(OpCodes.Ldtoken, parameters[i].ParameterType);
                    body.Emit(OpCodes.Call, GetTypeFromHandle.Value);
                    body.Emit(OpCodes.Stelem_Ref);
                }
            }

            // propertySignature = GetProperty
            body.Emit(OpCodes.Call, GetProperty.Value);
            body.Emit(OpCodes.Stloc, propertySignatureVariable.LocalIndex);
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetMethod"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Type.GetMethod(string, System.Type[])"/> method. </returns>
        private static MethodInfo InitializeGetMethod()
        {
            var type = typeof(Type);
            var methodName = nameof(Type.GetMethod);
            var getMethod = @type.GetMethod(methodName, new[] { typeof(string), typeof(Type[]) });
            return getMethod ?? throw new MethodInfoException(type, methodName);
        }

        /// <summary>
        /// Initialization logic for the <see cref="MakeByRefType"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Type.MakeByRefType()"/> method. </returns>
        private static MethodInfo InitializeMakeByRefType()
        {
            var type = typeof(Type);
            var methodName = nameof(Type.MakeByRefType);
            var makeByRefType = @type.GetMethod(methodName, Array.Empty<Type>());
            return makeByRefType ?? throw new MethodInfoException(type, methodName);
        }

        /// <summary>
        /// Initialization logic for the <see cref="ArrayEmptyType"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Array.Empty{T}"/> method. </returns>
        private static MethodInfo InitializeArrayEmptyType()
        {
            var type = typeof(Array);
            var methodName = nameof(Array.Empty);
            var openGenericArrayEmpty = @type.GetMethod(methodName, 1, Array.Empty<Type>());
            if (openGenericArrayEmpty == null)
            {
                throw new MethodInfoException(type, methodName);
            }
            var arrayEmpty = openGenericArrayEmpty.MakeGenericMethod(typeof(Type));
            return arrayEmpty ?? throw new MethodInfoException(type, methodName);
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetProperty"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="Type.GetProperty(string, Type[])"/> method. </returns>
        private static MethodInfo InitializeGetProperty()
        {
            var type = typeof(Type);
            var propertyName = nameof(Type.GetProperty);
            var getProperty = @type.GetMethod(propertyName, new[] { typeof(string), typeof(Type[]) });
            return getProperty ?? throw new PropertyInfoException(type, propertyName);
        }

        #endregion
    }
}