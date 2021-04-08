namespace CustomCode.AutomatedTesting.Mocks.Emitter
{
    using ExceptionHandling;
    using Extensions;
    using Interception;
    using Interception.Parameters;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    /// <summary>
    /// Implementation of the <see cref="IPropertyEmitter"/> interface for emitting dynamic property getters and setters
    /// that will forward any calls to an injected <see cref="IInterceptor.Intercept(IInvocation)"/> instance.
    /// </summary>
    /// <typeparam name="T"> The type of the property. </typeparam>
    /// <remarks>
    /// Emits the following source code:
    ///
    /// <![CDATA[
    ///     var propertySignature = typeof(Interface).GetProperty(
    ///         nameof(Property));
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(get_Property),
    ///         Array.Empty<Type>());
    ///
    ///     var propertyFeature = new PropertyInvocation(propertySignature);
    ///     var returnValueFeature = new ReturnValueInvocation<T>();
    ///
    ///     var incovation = new Invocation(methodSignature, propertyFeature, returnValueFeature);
    ///     _interceptor.Intercept(incovation);
    ///     return returnValueFeature.ReturnValue;
    /// ]]>
    /// <![CDATA[
    ///     var propertySignature = typeof(Interface).GetProperty(
    ///         nameof(Property));
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(set_Property),
    ///         Array.Empty<Type>());
    ///
    ///     var propertySetterValueFeature = new PropertySetterValue(propertySignature, value);
    ///
    ///     var incovation = new Invocation(methodSignature, propertySetterValueFeature);
    ///     _interceptor.Intercept(incovation);
    ///     return;
    /// ]]>
    ///
    /// or
    ///
    /// <![CDATA[
    ///     var propertySignature = typeof(Interface).GetProperty(
    ///         nameof(Property));
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(get_Property),
    ///         new[] { typeof(parameter1), ... typeof(parameterN) });
    ///
    ///     var propertyFeature = new PropertyInvocation(propertySignature);
    ///     var returnValueFeature = new ReturnValueInvocation<T>();
    ///     var parameterInFeature = new ParameterIn(methodSignature, new[] { parameter1, ...  parameterN });
    ///
    ///     var incovation = new Invocation(methodSignature, propertyFeature, returnValueFeature, parameterInFeature);
    ///     _interceptor.Intercept(incovation);
    ///     return returnValueFeature.ReturnValue;
    /// ]]>
    /// <![CDATA[
    ///     var propertySignature = typeof(Interface).GetProperty(
    ///         nameof(Property));
    ///     var methodSignature = typeof(Interface).GetMethod(
    ///         nameof(set_Property),
    ///         new[] { typeof(parameter1), ... typeof(parameterN) });
    ///
    ///     var propertySetterValueFeature = new PropertySetterValue(propertySignature, value);
    ///     var parameterInFeature = new ParameterIn(methodSignature, new[] { parameter1, ...  parameterN });
    ///
    ///     var incovation = new Invocation(methodSignature, propertySetterValueFeature, parameterInFeature);
    ///     _interceptor.Intercept(incovation);
    ///     return;
    /// ]]>
    /// </remarks>
    public sealed class InterceptGetterSetterEmitter<T> : PropertyEmitterBase
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="InterceptGetterSetterEmitter{T}"/> type.
        /// </summary>
        /// <param name="type"> The dynamic proxy type. </param>
        /// <param name="signature"> The signature of the property to be created. </param>
        /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
        public InterceptGetterSetterEmitter(TypeBuilder type, PropertyInfo signature, FieldBuilder interceptorField)
            : base(type, signature, interceptorField)
        { }

        #endregion

        #region Logic

        /// <inheritdoc cref="PropertyEmitterBase" />
        public override void EmitPropertyImplementation()
        {
            var features = new List<LocalBuilder>();
            var parameters = Signature.GetIndexParameters();
            var types = parameters.Select(p => p.ParameterType).ToArray();
            var typesAndValue = types.Concat(new[] { Signature.PropertyType }).ToArray();
            var inParameters = parameters.Where(p => !p.IsOut && !p.ParameterType.IsByRef).ToArray();

            var property = Type.DefineProperty(
                Signature.Name,
                PropertyAttributes.None,
                Signature.PropertyType,
                types);

            var getterSignature = Signature.GetGetMethod() ?? throw new MethodInfoException(Type, $"get_{Signature.Name}");
            var getter = Type.DefineMethod(
                getterSignature.Name,
                MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                Signature.PropertyType,
                types);
            var body = getter.GetILGenerator();

            // local variables
            body.EmitLocalPropertySignatureVariable(out var getterPropertySignature);
            body.EmitLocalMethodSignatureVariable(out var getterMethodSignature);
            body.EmitLocalPropertyFeatureVariable(out var propertyFeature);
            features.Add(propertyFeature);
            body.EmitLocalReturnValueFeatureVariable<T>(out var returnValue);
            features.Add(returnValue);
            if (inParameters.Length > 0)
            {
                features.Add(body.EmitLocalParameterFeatureVariable<ParameterIn>());
            }
            body.EmitLocalInvocationVariable(out var getterInvocation);

            // body
            body.EmitGetPropertySignature(Signature, getterPropertySignature);
            body.EmitGetMethodSignature(getterSignature, getterMethodSignature);
            body.EmitNewPropertyFeature(getterPropertySignature, propertyFeature);
            body.EmitNewReturnValueFeature<T>(returnValue);
            if (inParameters.Length > 0)
            {
                body.EmitNewParameterFeature<ParameterIn>(getterMethodSignature, parameters, features[2]);
            }
            body.EmitNewInvocation(getterInvocation, getterMethodSignature, features);
            body.EmitInterceptCall(InterceptorField, getterInvocation);
            body.EmitReturnStatement<T>(returnValue);

            property.SetGetMethod(getter);



            features.Clear();
            var setterSignature = Signature.GetSetMethod() ?? throw new MethodInfoException(Type, $"set_{Signature.Name}");
            var setter = Type.DefineMethod(
                setterSignature.Name,
                MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                null,
                typesAndValue);
            body = setter.GetILGenerator();

            // local variables
            body.EmitLocalPropertySignatureVariable(out var setterPropertySignature);
            body.EmitLocalMethodSignatureVariable(out var setterMethodSignature);
            body.EmitLocalPropertySetterValueFeatureVariable(out var setterValue);
            features.Add(setterValue);
            if (inParameters.Length > 0)
            {
                features.Add(body.EmitLocalParameterFeatureVariable<ParameterIn>());
            }
            body.EmitLocalInvocationVariable(out var setterInvocation);

            // body
            body.EmitGetPropertySignature(Signature, setterPropertySignature);
            body.EmitGetMethodSignature(setterSignature, setterMethodSignature);
            body.EmitNewPropertySetterValueFeature(Signature, setterPropertySignature, setterValue, typesAndValue.Length);
            if (inParameters.Length > 0)
            {
                body.EmitNewParameterFeature<ParameterIn>(setterMethodSignature, parameters, features[1]);
            }
            body.EmitNewInvocation(setterInvocation, setterMethodSignature, features);
            body.EmitInterceptCall(InterceptorField, setterInvocation);
            body.EmitReturnStatement();

            property.SetSetMethod(setter);
        }

        #endregion
    }
}