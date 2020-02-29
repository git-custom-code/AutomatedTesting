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
            body.EmitLocalPropertySignatureVariable(out var propertySignatureVariable);
            body.EmitLocalSetterInvocationVariable(out var invocationVariable);

            // body
            body.EmitGetPropertySignature(Signature, propertySignatureVariable);
            body.EmitNewSetterInvocation(Signature.PropertyType, propertySignatureVariable, invocationVariable);
            body.EmitInterceptCall(InterceptorField, invocationVariable);
            EmitReturnStatement(body);

            property.SetSetMethod(setter);
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

        #endregion
    }
}