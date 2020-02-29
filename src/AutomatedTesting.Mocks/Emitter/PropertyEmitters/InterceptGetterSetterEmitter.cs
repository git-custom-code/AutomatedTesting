namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Extensions;
    using Interception;
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IPropertyEmitter"/> interface for emitting dynamic property getters and setters
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
    /// <![CDATA[
    ///     var propertySignature = typeof(Interface).GetProperty(nameof(Property));
    ///     var invocation = new SetterInvocation(propertySignature);
    ///     _interceptor.Intercept(invocation);
    ///
    ///     return;
    /// ]]>
    /// </remarks>
    public sealed class InterceptGetterSetterEmitter : PropertyEmitterBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="InterceptGetterSetterEmitter"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the property to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public InterceptGetterSetterEmitter(TypeBuilder type, PropertyInfo signature, FieldBuilder interceptorField)
            : base (type, signature, interceptorField)
        { }

        #endregion

        #region Data

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
            body.EmitLocalPropertySignatureVariable(out var getterPropertySignatureVariable);
            body.EmitLocalGetterInvocationVariable(out var getterInvocationVariable);
            EmitLocalGetterReturnValue(body, out var returnValue);

            // body
            body.EmitGetPropertySignature(Signature, getterPropertySignatureVariable);
            body.EmitNewGetterInvocation(getterPropertySignatureVariable, getterInvocationVariable);
            body.EmitInterceptCall(InterceptorField, getterInvocationVariable);
            EmitGetterReturnStatement(body, getterInvocationVariable, returnValue);

            property.SetGetMethod(getter);

            var setter = Type.DefineMethod(
                Signature.GetSetMethod()?.Name ?? $"set_{Signature.Name}",
                MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                null,
                new Type[] { Signature.PropertyType });
            body = setter.GetILGenerator();

            // local variables
            body.EmitLocalPropertySignatureVariable(out var setterPropertySignatureVariable);
            body.EmitLocalSetterInvocationVariable(out var setterInvocationVariable);

            // body
            body.EmitGetPropertySignature(Signature, setterPropertySignatureVariable);
            body.EmitNewSetterInvocation(Signature.PropertyType, setterPropertySignatureVariable, setterInvocationVariable);
            body.EmitInterceptCall(InterceptorField, setterInvocationVariable);
            EmitSetterReturnStatement(body);

            property.SetSetMethod(setter);
        }

        /// <summary>
        /// Emits the following source code:
        /// <![CDATA[
        ///     PropertyType returnValue;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's get method. </param>
        /// <param name="returnValue"> The emitted local return value. </param>
        private void EmitLocalGetterReturnValue(ILGenerator body, out LocalBuilder returnValue)
        {
            returnValue = body.DeclareLocal(Signature.PropertyType);
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
        private void EmitGetterReturnStatement(ILGenerator body, LocalBuilder invocationVariable, LocalBuilder returnValue)
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
        /// Emits the following source code:
        /// <![CDATA[
        ///     return;
        /// ]]>
        /// </summary>
        /// <param name="body"> The body of the dynamic property's set method. </param>
        private void EmitSetterReturnStatement(ILGenerator body)
        {
            var label = body.DefineLabel();

            body.Emit(OpCodes.Nop);
            body.Emit(OpCodes.Br_S, label);
            body.MarkLabel(label);
            body.Emit(OpCodes.Ret);
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