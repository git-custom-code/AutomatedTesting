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
    ///
    ///     var incovation = new Invocation(methodSignature, parameterInFeature);
    ///     _interceptor.Intercept(incovation);
    ///
    ///     parameterRef1 = parameterRefFeatre.GetValue<Type1>("Name1");
    ///     ...
    ///     parameterRefY = parameterRefFeatre.GetValue<TypeY>("NameY");
    ///
    ///     return;
    /// ]]>
    /// </remarks>
    public sealed class InterceptActionEmitter : MethodEmitterBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="InterceptActionEmitter"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the method to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public InterceptActionEmitter(TypeBuilder type, MethodInfo signature, FieldBuilder interceptorField)
            : base(type, signature, interceptorField)
        { }

        #endregion

        #region Logic

        /// <inheritdoc />
        public override void EmitMethodImplementation()
        {
            var features = new List<LocalBuilder>();
            var parameters = Signature.GetParameters();
            var inParameters = parameters.Where(p => !p.IsOut && !p.ParameterType.IsByRef).ToArray();
            var refParameters = parameters.Where(p => p.ParameterType.IsByRef).ToArray();
            // ToDo: Out

            var method = Type.DefineMethod(
                Signature.Name,
                MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Final,
                Signature.ReturnType,
                Signature.GetParameters().Select(p => p.ParameterType).ToArray());
            var body = method.GetILGenerator();

            // local variables
            body.EmitLocalMethodSignatureVariable(out var methodSignatureVariable);

            if (inParameters.Length > 0)
            {
                body.EmitLocalParameterFeatureVariable<ParameterIn>(out var parameterInFeature);
                features.Add(parameterInFeature);
            }
            if (refParameters.Length > 0)
            {
                body.EmitLocalParameterFeatureVariable<ParameterRef>(out var parameterRefFeature);
                features.Add(parameterRefFeature);
            }

            body.EmitLocalInvocationVariable(out var invocationVariable);

            // body
            body.EmitGetMethodSignature(Signature, methodSignatureVariable);

            if (inParameters.Length > 0)
            {
                body.EmitNewParameterFeature<ParameterIn>(methodSignatureVariable, parameters, features[0]);
            }
            if (refParameters.Length > 0)
            {
                var index = (inParameters.Length > 0) ? 1 : 0;
                body.EmitNewParameterFeature<ParameterRef>(methodSignatureVariable, parameters, features[index]);
            }

            body.EmitNewInvocation(invocationVariable, methodSignatureVariable, features);
            body.EmitInterceptCall(InterceptorField, invocationVariable);

            if (refParameters.Length > 0)
            {
                var index = (inParameters.Length > 0) ? 1 : 0;
                foreach (var parameter in refParameters)
                {
                    body.EmitSyncRefParameter(parameter, features[index]);
                }
            }

            body.EmitReturnStatement();
        }

        #endregion
    }
}