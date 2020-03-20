namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using Extensions;
    using Interception;
    using Interception.Async;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of the <see cref="IMethodEmitter"/> interface for emitting dynamic methods
    /// for asynchronous methods (i.e. methods that return either <see cref="Task"/>, <see cref="Task{TResult}"/>,
    /// <see cref="ValueTask"/>, <see cref="ValueTask{TResult}"/> or <see cref="IAsyncEnumerable{T}"/>)
    /// that will forward any calls to an injected <see cref="IInterceptor.Intercept(IInvocation)"/> instance.
    /// </summary>
    /// <remarks>
    /// Emits the following source code:
    ///
    /// <![CDATA[
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(Method),
    ///         Array.Empty<Type>());
    ///
    ///     var asyncFeature = new Async...Invocation();
    /// 
    ///     var incovation = new Invocation(methodSignature, asyncFeature);
    ///     _interceptor.Intercept(incovation);
    ///     return asyncFeature.AsyncReturnValue;
    /// ]]>
    ///
    /// or
    ///
    /// <![CDATA[
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(Method),
    ///         new[] { typeof(parameter1), ... typeof(parameterN) });
    ///
    ///     var parameterInFeature = new ParameterIn(methodSignature, new[] { parameter1, ...  parameterN });
    ///     var asyncFeature = new Async...Invocation();
    ///
    ///     var incovation = new Invocation(methodSignature, parameterInFeature, asyncFeature);
    ///     _interceptor.Intercept(incovation);
    ///     return asyncFeature.AsyncReturnValue;
    /// ]]>
    /// </remarks>
    public sealed class InterceptAsyncMethodEmitter<T> : MethodEmitterBase
        where T : IAsyncInvocation
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="InterceptAsyncMethodEmitter{T}"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the asynchronous method to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public InterceptAsyncMethodEmitter(TypeBuilder type, MethodInfo signature, FieldBuilder interceptorField)
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
                body.EmitLocalParameterInFeatureVariable(out var parameterInFeature);
                features.Add(parameterInFeature);
            }

            body.EmitLocalAsyncFeatureVariable<T>(out var asyncFeature);
            features.Add(asyncFeature);

            body.EmitLocalInvocationVariable(out var invocationVariable);

            // body
            body.EmitGetMethodSignature(Signature, methodSignatureVariable);

            if (inParameters.Length > 0)
            {
                body.EmitNewParameterInFeature(methodSignatureVariable, parameters, features[0]);
            }

            body.EmitNewAsyncFeature<T>(asyncFeature);

            body.EmitNewInvocation(invocationVariable, methodSignatureVariable, features);
            body.EmitInterceptCall(InterceptorField, invocationVariable);
            body.EmitAsyncReturnStatement<T>(asyncFeature);
        }

        #endregion
    }
}