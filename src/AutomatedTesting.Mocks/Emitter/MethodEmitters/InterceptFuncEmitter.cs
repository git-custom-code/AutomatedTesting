namespace CustomCode.AutomatedTesting.Mocks.Emitter;

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
///     var returnValueFeature = new ReturnValueInvocation<T>();
///
///     var incovation = new Invocation(methodSignature, returnValueFeature);
///     _interceptor.Intercept(incovation);
///     return returnValueFeature.ReturnValue;
/// ]]>
///
/// or
///
/// <![CDATA[
///     var methodSignature = typeof(Interface).GetMethod(
///         nameof(Method),
///         new[] { typeof(parameter1), ... typeof(parameterN) });
///
///     var returnValueFeature = new ReturnValueInvocation<T>();
///     var parameterInFeature = new ParameterIn(methodSignature, new[] { parameterIn1, ...  parameterInX });
///     var parameterRefFeature = new ParameterRef(methodSignature, new[] { parameterRef1, ...  parameterRefY });
///     var parameterOutFeature = new ParameterOut(methodSignature);
///     
///     var incovation = new Invocation(methodSignature, returnValueFeature, parameterInFeature, parameterRefFeature, parameterOutFeature);
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
///     return returnValueFeature.ReturnValue;
/// ]]>
/// </remarks>
public sealed class InterceptFuncEmitter<T> : MethodEmitterBase
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="InterceptFuncEmitter{T}"/> type.
    /// </summary>
    /// <param name="type"> The dynamic proxy type. </param>
    /// <param name="signature"> The signature of the method to be created. </param>
    /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
    public InterceptFuncEmitter(TypeBuilder type, MethodInfo signature, FieldBuilder interceptorField)
        : base(type, signature, interceptorField)
    { }

    #endregion

    #region Logic

    /// <inheritdoc cref="MethodEmitterBase" />
    public override void EmitMethodImplementation()
    {
        var features = new List<LocalBuilder>();
        var parameters = Signature.GetParameters();
        var parameterIn = (LocalBuilder?)null;
        var inParameters = parameters.Where(p => !p.IsOut && !p.ParameterType.IsByRef).ToArray();
        var parameterRef = (LocalBuilder?)null;
        var refParameters = parameters.Where(p => !p.IsOut && p.ParameterType.IsByRef).ToArray();
        var parameterOut = (LocalBuilder?)null;
        var outParameters = parameters.Where(p => p.IsOut && p.ParameterType.IsByRef).ToArray();

        var method = Type.DefineMethod(
            Signature.Name,
            MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Final,
            Signature.ReturnType,
            parameters.Select(p => p.ParameterType).ToArray());
        var body = method.GetILGenerator();

        // local variables
        body.EmitLocalMethodSignatureVariable(out var signature);
        body.EmitLocalReturnValueFeatureVariable<T>(out var returnValue);
        features.Add(returnValue);
        if (inParameters.Length > 0)
        {
            parameterIn = body.EmitLocalParameterFeatureVariable<ParameterIn>();
            features.Add(parameterIn);
        }
        if (refParameters.Length > 0)
        {
            parameterRef = body.EmitLocalParameterFeatureVariable<ParameterRef>();
            features.Add(parameterRef);
        }
        if (outParameters.Length > 0)
        {
            parameterOut = body.EmitLocalParameterFeatureVariable<ParameterOut>();
            features.Add(parameterOut);
        }
        body.EmitLocalInvocationVariable(out var invocation);

        // body
        body.EmitGetMethodSignature(Signature, signature);
        body.EmitNewReturnValueFeature<T>(returnValue);
        if (parameterIn != null)
        {
            body.EmitNewParameterFeature<ParameterIn>(signature, parameters, parameterIn);
        }
        if (parameterRef != null)
        {
            body.EmitNewParameterFeature<ParameterRef>(signature, parameters, parameterRef);
        }
        if (parameterOut != null)
        {
            body.EmitNewParameterFeature<ParameterOut>(signature, parameters, parameterOut);
        }
        body.EmitNewInvocation(invocation, signature, features);
        body.EmitInterceptCall(InterceptorField, invocation);
        if (parameterRef != null)
        {
            body.EmitSyncParameter<ParameterRef>(refParameters, parameterRef);
        }
        if (parameterOut != null)
        {
            body.EmitSyncParameter<ParameterOut>(outParameters, parameterOut);
        }
        body.EmitReturnStatement<T>(returnValue);
    }

    #endregion
}
