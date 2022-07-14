namespace CustomCode.AutomatedTesting.Mocks.Emitter;

using ExceptionHandling;
using Interception;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;

/// <summary>
/// Default implementation of the <see cref="IPropertyDecoratorEmitterFactory"/> interface.
/// </summary>
public sealed class PropertyDecoratorEmitterFactory : IPropertyDecoratorEmitterFactory
{
    #region Data

    /// <summary>
    /// Gets a thread-safe cache that is used to store factories for creating strongly typed
    /// <see cref="IPropertyEmitter"/> instances for getter only properties.
    /// </summary>
    private static ConcurrentDictionary<Type, PropertyEmitterDelegate> GetterEmitterCache { get; }
        = new ConcurrentDictionary<Type, PropertyEmitterDelegate>();

    /// <summary>
    /// Gets a thread-safe cache that is used to store factories for creating strongly typed
    /// <see cref="IPropertyEmitter"/> instances for getter/setter properties.
    /// </summary>
    private static ConcurrentDictionary<Type, PropertyEmitterDelegate> GetterSetterEmitterCache { get; }
        = new ConcurrentDictionary<Type, PropertyEmitterDelegate>();

    #endregion

    #region Logic

    /// <inheritdoc cref="IPropertyDecoratorEmitterFactory" />
    public IPropertyEmitter CreatePropertyEmitterFor(
        PropertyInfo signature,
        TypeBuilder type,
        FieldBuilder decoratee,
        FieldBuilder interceptor)
    {
        Ensures.NotNull(signature, nameof(signature));
        Ensures.NotNull(type, nameof(type));
        Ensures.NotNull(decoratee, nameof(decoratee));
        Ensures.NotNull(interceptor, nameof(interceptor));

        if (signature.CanRead)
        {
            if (signature.CanWrite)
            {
                var factory = GetterSetterEmitterCache.GetOrAdd(
                    signature.PropertyType,
                    CreateEmitterFactoryFor(signature.PropertyType, typeof(DecorateGetterSetterEmitter<>)));
                return factory(type, signature, decoratee, interceptor);
            }
            else
            {
                var factory = GetterEmitterCache.GetOrAdd(
                    signature.PropertyType,
                    CreateEmitterFactoryFor(signature.PropertyType, typeof(DecorateGetterEmitter<>)));
                return factory(type, signature, decoratee, interceptor);
            }
        }
        else if (signature.CanWrite)
        {
            return new DecorateSetterEmitter(type, signature, decoratee, interceptor);
        }

        throw new NotSupportedException();
    }

    /// <summary>
    /// Use a <see cref="DynamicMethod"/> to create a factory delegate for a strongly typed
    /// <see cref="DecorateGetterEmitter{T}" /> or <see cref="DecorateGetterSetterEmitter{T}"/>.
    /// </summary>
    /// <param name="returnType">
    /// The type of the property's return value.
    /// </param>
    /// <param name="emitterType"> Type type of the emitter to be created. </param>
    /// <returns> The created factory delegate. </returns>
    /// <remarks>
    /// Emits the following source code:
    /// <![CDATA[
    ///     IPropertyEmitter Create(TypeBuilder type, PropertyInfo signature, FieldBuilder decoratee, FieldBuilder interceptor)
    ///     {
    ///         return new Decorate...Emitter<ReturnType>(type, signature, decoratee, interceptor);
    ///     }
    /// ]]>
    /// </remarks>
    private PropertyEmitterDelegate CreateEmitterFactoryFor(Type returnType, Type emitterType)
    {
        var genericEmitterType = emitterType.MakeGenericType(returnType);
        var ctor = genericEmitterType
            .GetConstructor(new[] { typeof(TypeBuilder), typeof(PropertyInfo), typeof(FieldBuilder), typeof(FieldBuilder) })
            ?? throw new ConstructorInfoException(genericEmitterType);

        var dynamicFactory = new DynamicMethod(
            $"Create{genericEmitterType.Name}",
            typeof(IPropertyEmitter),
            new[] { typeof(TypeBuilder), typeof(PropertyInfo), typeof(FieldBuilder), typeof(FieldBuilder) },
            genericEmitterType);
        var body = dynamicFactory.GetILGenerator();

        body.Emit(OpCodes.Ldarg_0);
        body.Emit(OpCodes.Ldarg_1);
        body.Emit(OpCodes.Ldarg_2);
        body.Emit(OpCodes.Ldarg_3);
        body.Emit(OpCodes.Newobj, ctor);
        body.Emit(OpCodes.Ret);

        var factory = (PropertyEmitterDelegate)dynamicFactory.CreateDelegate(typeof(PropertyEmitterDelegate));
        return factory;
    }

    #endregion

    #region Nested Types

    /// <summary>
    /// A delegate that defines a factory for creating a strongly typed <see cref="IPropertyEmitter"/>
    /// instance.
    /// </summary>
    /// <param name="type"> The dynamic proxy type. </param>
    /// <param name="signature"> The signature of the property to be created. </param>
    /// <param name="decorateeField"> The <paramref name="type"/>'s decoratee backing field. </param>
    /// <param name="interceptorField"> The <paramref name="type"/>'s <see cref="IInterceptor"/> backing field. </param>
    /// <returns> The created <see cref="IPropertyEmitter"/> instance. </returns>
    private delegate IPropertyEmitter PropertyEmitterDelegate(
        TypeBuilder type,
        PropertyInfo signature,
        FieldBuilder decorateeField,
        FieldBuilder interceptorField);

    #endregion
}
