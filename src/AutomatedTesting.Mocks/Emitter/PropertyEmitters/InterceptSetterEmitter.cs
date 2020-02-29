namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Extensions;
    using Interception;
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IPropertyEmitter"/> interface for emitting dynamic property setters
    /// that will forward any calls to an injected <see cref="IInterceptor.Intercept(IInvocation)"/> instance.
    /// </summary>
    /// <remarks>
    /// Emits the following source code:
    /// <![CDATA[
    ///     var propertySignature = typeof(Interface).GetProperty(nameof(Property));
    ///     var invocation = new SetterInvocation(propertySignature);
    ///     _interceptor.Intercept(invocation);
    ///
    ///     return;
    /// ]]>
    /// </remarks>
    public sealed class InterceptSetterEmitter : PropertyEmitterBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="InterceptSetterEmitter"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the property to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public InterceptSetterEmitter(TypeBuilder type, PropertyInfo signature, FieldBuilder interceptorField)
            : base(type, signature, interceptorField)
        { }

        #endregion

        #region Data

        /// <summary>
        /// Gets the cached signature of the <see cref="SetterInvocation"/> constructor.
        /// </summary>
        private static Lazy<ConstructorInfo> SetterInvocationConstructor { get; } = new Lazy<ConstructorInfo>(InitializeSetterInvocationConstructor, true);

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

            var setter = Type.DefineMethod(
                Signature.GetSetMethod()?.Name ?? $"set_{Signature.Name}",
                MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                null,
                new Type[] { Signature.PropertyType });
            var body = setter.GetILGenerator();

            // local variables
            EmitLocalPropertySignatureVariable(body, out var propertySignatureVariable);
            EmitLocalInvocationVariable(body, out var invocationVariable);

            // body
            EmitGetPropertySignature(body, propertySignatureVariable);
            EmitNewSetterInvocation(body, propertySignatureVariable, invocationVariable);
            body.EmitInterceptCall(InterceptorField, invocationVariable);
            EmitReturnStatement(body);

            property.SetSetMethod(setter);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     SetterInvocation invocation;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's set method. </param>
        /// <param name="invocationVariable"> The emitted local <see cref="SetterInvocation"/> variable. </param>
        private void EmitLocalInvocationVariable(ILGenerator body, out LocalBuilder invocationVariable)
        {
            invocationVariable = body.DeclareLocal(typeof(SetterInvocation));
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     invocation = new SetterInvocation(propertySignature, value);
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's set method. </param>
        /// <param name="propertySignatureVariable"> The emitted local <see cref="PropertyInfo"/> variable. </param>
        /// <param name="invocationVariable"> The local <see cref="SetterInvocation"/> variable. </param>
        private void EmitNewSetterInvocation(
            ILGenerator body,
            LocalBuilder propertySignatureVariable,
            LocalBuilder invocationVariable)
        {
            body.Emit(OpCodes.Ldloc, propertySignatureVariable.LocalIndex);
            body.Emit(OpCodes.Ldarg_1);
            if (Signature.PropertyType.IsValueType)
            {
                body.Emit(OpCodes.Box, Signature.PropertyType);
            }
            body.Emit(OpCodes.Newobj, SetterInvocationConstructor.Value);
            body.Emit(OpCodes.Stloc, invocationVariable.LocalIndex);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     return;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's set method. </param>
        private void EmitReturnStatement(ILGenerator body)
        {
            var label = body.DefineLabel();

            body.Emit(OpCodes.Nop);
            body.Emit(OpCodes.Br_S, label);
            body.MarkLabel(label);
            body.Emit(OpCodes.Ret);
        }

        /// <summary>
        /// Initialization logic for the <see cref="SetterInvocationConstructor"/> property.
        /// </summary>
        /// <returns> The signature of the <see cref="SetterInvocation"/> constructor. </returns>
        private static ConstructorInfo InitializeSetterInvocationConstructor()
        {
            var constructor = typeof(SetterInvocation).GetConstructor(new[] { typeof(PropertyInfo), typeof(object) });
            return constructor ?? throw new ArgumentNullException(nameof(SetterInvocationConstructor));
        }

        #endregion
    }
}