namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Extensions;
    using Interception;
    using Interception.Parameters;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IMethodEmitter"/> interface for emitting dynamic methods with
    /// return type <see cref="void"/> and without any out or ref input parameters that will forward any calls
    /// to an injected <see cref="IInterceptor.Intercept(IInvocation)"/> instance.
    /// </summary>
    /// <remarks>
    /// Emits the following source code:
    ///
    /// <![CDATA[
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(Method),
    ///         Array.Empty<Type>());
    ///
    ///     var incovation = new Invocation(methodSignature);
    ///     _interceptor.Intercept(incovation);
    ///     return;
    /// ]]>
    ///
    /// or
    ///
    /// <![CDATA[
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(Method),
    ///         new[] { typeof(parameter1), ... typeof(parameterN) });
    ///
    ///     var parameterInFeature = new ParameterIn(methodSignature, new[] { parameterIn1, ...  parameterInX });
    ///     var parameterRefFeature = new ParameterRef(methodSignature, new[] { parameterRef1, ...  parameterRefY });
    ///     var parameterOutFeature = new ParameterOut(methodSignature);
    ///
    ///     var incovation = new Invocation(methodSignature, parameterInFeature, parameterRefFeature, parameterOutFeature);
    ///     _interceptor.Intercept(incovation);
    ///
    ///     parameterRef1 = parameterRefFeature.GetValue<Type1>("Name1");
    ///     ...
    ///     parameterRefY = parameterRefFeature.GetValue<TypeY>("NameY");
    ///
    ///     parameterOut1 = parameterOutFeature.GetValue<Type1>("Name1");
    ///     ...
    ///     parameterOutZ = parameterOutFeature.GetValue<TypeY>("NameZ");
    ///
    ///     return;
    /// ]]>
    /// </remarks>
    public sealed class DecorateActionEmitter : MethodDecoratorEmitterBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="DecorateActionEmitter"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the method to be created. </param>
        /// <param name="decorateeField"> The <paramref name="type"/>'s decoratee backing field. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public DecorateActionEmitter(
            TypeBuilder type,
            MethodInfo signature,
            FieldBuilder decorateeField,
            FieldBuilder interceptorField)
            : base(type, signature, decorateeField, interceptorField)
        { }

        #endregion

        #region Logic

        /// <inheritdoc />
        public override void EmitMethodImplementation()
        {
            var parameters = Signature.GetParameters();

            var method = Type.DefineMethod(
                Signature.Name,
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Final,
                Signature.ReturnType,
                parameters.Select(p => p.ParameterType).ToArray());
            var body = method.GetILGenerator();

            // local variables

            // body
            body.EmitDecorateeCall(Signature, parameters, DecorateeField);
            body.EmitReturnStatement();
        }

        #endregion
    }
}