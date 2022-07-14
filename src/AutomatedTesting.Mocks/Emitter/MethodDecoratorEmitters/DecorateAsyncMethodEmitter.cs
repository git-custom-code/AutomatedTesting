namespace CustomCode.AutomatedTesting.Mocks.Emitter;

using Extensions;
using Interception;
using Interception.Async;
using Interception.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;

/// <summary>
/// Implementation of the <see cref="IMethodEmitter"/> interface for emitting dynamic methods
/// for asynchronous methods (i.e. methods that return either <see cref="Task"/>, <see cref="Task{TResult}"/>,
/// <see cref="ValueTask"/>, <see cref="ValueTask{TResult}"/> or <see cref="IAsyncEnumerable{T}"/>)
/// that will forward any calls to either an injected <see cref="IInterceptor.Intercept(IInvocation)"/>
/// instance or to the original implementation of a decoratee instance.
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
///     if (!_interceptor.Intercept(incovation))
///     {
///         return _decoratee.Method(parameter1, ... parameterN);
///     }
///
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
///     if (!_interceptor.Intercept(incovation))
///     {
///         return _decoratee.Method(parameter1, ... parameterN);
///     }
///
///     return asyncFeature.AsyncReturnValue;
/// ]]>
/// </remarks>
public sealed class DecorateAsyncMethodEmitter<T> : MethodDecoratorEmitterBase
    where T : IAsyncInvocation
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="DecorateAsyncMethodEmitter{T}"/> type.
    /// </summary>
    /// <param name="type"> The dynamic proxy type. </param>
    /// <param name="signature"> The signature of the asynchronous method to be created. </param>
    /// <param name="decorateeField"> The <paramref name="type"/>'s decoratee backing field. </param>
    /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
    public DecorateAsyncMethodEmitter(
        TypeBuilder type,
        MethodInfo signature,
        FieldBuilder decorateeField,
        FieldBuilder interceptorField)
        : base(type, signature, decorateeField, interceptorField)
    { }

    #endregion

    #region Logic

    /// <inheritdoc cref="MethodDecoratorEmitterBase" />
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
        body.EmitLocalMethodSignatureVariable(out var methodSignature);

        if (inParameters.Length > 0)
        {
            features.Add(body.EmitLocalParameterFeatureVariable<ParameterIn>());
        }

        body.EmitLocalAsyncFeatureVariable<T>(out var asyncFeature);
        features.Add(asyncFeature);

        body.EmitLocalInvocationVariable(out var invocation);

        // body
        body.EmitGetMethodSignature(Signature, methodSignature);

        if (inParameters.Length > 0)
        {
            body.EmitNewParameterFeature<ParameterIn>(methodSignature, parameters, features[0]);
        }

        body.EmitNewAsyncFeature<T>(asyncFeature);

        body.EmitNewInvocation(invocation, methodSignature, features);
        body.EmitIfInterceptCall(InterceptorField, invocation, out var elseLabel);
        body.EmitDecorateeCall(Signature, parameters, DecorateeField);
        body.EmitReturnStatement();
        body.EmitElseInterceptCall(elseLabel);
        body.EmitAsyncReturnStatement<T>(asyncFeature);
    }

    #endregion
}
