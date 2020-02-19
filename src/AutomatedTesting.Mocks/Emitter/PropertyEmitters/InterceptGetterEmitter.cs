namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Interception;
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IPropertyEmitter"/> interface for emitting dynamic property getters
    /// that will forward any calls to an injected <see cref="IInterceptor.Intercept(IInvocation)"/> instance.
    /// </summary>
    /// <remarks>
    /// Emits the following source code:
    /// <![CDATA[
    ///     var propertySignature = typeof(Interface).GetProperty(nameof(Property));
    ///     var invocation = new GetterInvocation(propertySignature);
    ///     _interceptor.Intercept(invocation);
    ///
    ///     return (PropertyType)invocation.ReturnValue;
    /// ]]>
    /// </remarks>
    public sealed class InterceptGetterEmitter : PropertyEmitterBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="InterceptGetterEmitter"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the property to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public InterceptGetterEmitter(TypeBuilder type, PropertyInfo signature, FieldBuilder interceptorField)
            : base (type, signature, interceptorField)
        { }

        #endregion

        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="GetterInvocation"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> GetterInvocationConstructor { get; } = new Lazy<ConstructorInfo>(InitializeGetterInvocationConstructor, true);

        /// <summary>
        /// Gets the cached signature of the <see cref="GetterInvocation.ReturnValue"/> getter.
        /// </summary>
        private static Lazy<MethodInfo> GetReturnValue { get; } = new Lazy<MethodInfo>(InitializeGetReturnValue, true);

        #endregion

        #region Logic

        /// <inheritdoc />
        public override void EmitPropertyImplementation()
        {
            var property = Type.DefineProperty(
                Signature.Name,
                PropertyAttributes.None,
                Signature.PropertyType,
                null);

            var getter = Type.DefineMethod(
                Signature.GetGetMethod()?.Name ?? $"get_{Signature.Name}",
                MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                Signature.PropertyType,
                null);
            var body = getter.GetILGenerator();

            // local variables
            EmitLocalPropertySignatureVariable(body, out var propertySignatureVariable);
            EmitLocalInvocationVariable(body, out var invocationVariable);
            EmitLocalReturnValue(body, out var returnValue);

            // body
            EmitGetPropertySignature(body, propertySignatureVariable);
            EmitNewGetterInvocation(body, propertySignatureVariable, invocationVariable);
            EmitCallInterceptor(body, invocationVariable);
            EmitReturnStatement(body, invocationVariable, returnValue);

            property.SetGetMethod(getter);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     GetterInvocation invocation;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's get method. </param>
        /// <param name="invocationVariable"> The emitted local <see cref="GetterInvocation"/> variable. </param>
        private void EmitLocalInvocationVariable(ILGenerator body, out LocalBuilder invocationVariable)
        {
            invocationVariable = body.DeclareLocal(typeof(GetterInvocation));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     PropertyType returnValue;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's get method. </param>
        /// <param name="returnValue"> The emitted local return value. </param>
        private void EmitLocalReturnValue(ILGenerator body, out LocalBuilder returnValue)
        {
            returnValue = body.DeclareLocal(Signature.PropertyType);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     invocation = new GetterInvocation(propertySignature);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's get method. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        /// <param name="invocationVariable"> The local <see cref="GetterInvocation"/> variable. </param>
        private void EmitNewGetterInvocation(
            ILGenerator body,
            LocalBuilder propertySignatureVariable,
            LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldloc, propertySignatureVariable.LocalIndex);
            body.Emit(OpCodes.Newobj, GetterInvocationConstructor.Value);
            body.Emit(OpCodes.Stloc, invocationVariable.LocalIndex);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     return (PropertyType)invocation.ReturnValue;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's get method. </param>
        /// <param name="invocationVariable"> The local <see cref="GetterInvocation"/> variable. </param>
        /// <param name="returnValue"> The emitted local return value. </param>
        private void EmitReturnStatement(ILGenerator body, LocalBuilder invocationVariable, LocalBuilder returnValue)
        {
            var label = body.DefineLabel();

            body.Emit(OpCodes.Nop);
            body.Emit(OpCodes.Ldloc, invocationVariable.LocalIndex);
            body.Emit(OpCodes.Callvirt, GetReturnValue.Value);
            if (Signature.PropertyType.IsValueType)
            {
                body.Emit(OpCodes.Unbox_Any, Signature.PropertyType);
            }
            else
            {
                body.Emit(OpCodes.Castclass, Signature.PropertyType);
            }
            body.Emit(OpCodes.Stloc, returnValue.LocalIndex);
            body.Emit(OpCodes.Br_S, label);
            body.MarkLabel(label);
            body.Emit(OpCodes.Ldloc, returnValue.LocalIndex);
            body.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetterInvocationConstructor"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="GetterInvocation"/> constructor. </returns>
        private static ConstructorInfo InitializeGetterInvocationConstructor()
        {
            var constructor = typeof(GetterInvocation).GetConstructor(new[] { typeof(PropertyInfo) });
            return constructor ?? throw new ArgumentNullException(nameof(GetterInvocationConstructor));
        }

        /// <summary>
        /// Initialization logic for the <see cref="GetReturnValue"/> property.
        /// </summary>
        /// <returns> Thethe signature of the <see cref="GetterInvocation.ReturnValue"/> getter. </returns>
        private static MethodInfo InitializeGetReturnValue()
        {
            var getReturnValue = typeof(GetterInvocation).GetProperty(nameof(GetterInvocation.ReturnValue))?.GetGetMethod();
            return getReturnValue ?? throw new ArgumentNullException(nameof(GetReturnValue));
        }

        #endregion
    }
}