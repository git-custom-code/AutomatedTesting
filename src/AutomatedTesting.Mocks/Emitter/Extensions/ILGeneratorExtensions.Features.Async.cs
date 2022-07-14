namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions;

using ExceptionHandling;
using Interception.Async;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;

/// <summary>
/// Extension methods for the <see cref="ILGenerator"/> type.
/// </summary>
public static partial class ILGeneratorExtensions
{
    #region Data

    /// <summary>
    /// Gets a thread-safe cache for <see cref="IAsyncInvocation{T}"/> constructors.
    /// </summary>
    private static ConcurrentDictionary<Type, ConstructorInfo> AsyncFeatureCache { get; }
        = new ConcurrentDictionary<Type, ConstructorInfo>();

    /// <summary>
    /// Gets a thread-safe cache for <see cref="IAsyncInvocation{T}.AsyncReturnValue"/> getters.
    /// </summary>
    private static ConcurrentDictionary<Type, MethodInfo> AsyncFeatureReturnValueCache { get; }
        = new ConcurrentDictionary<Type, MethodInfo>();

    #endregion

    #region Logic

    /// <summary>
    /// Emits a local async feature variable of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="body"> The body of the dynamic method. </param>
    /// <param name="asyncFeatureVariable"> The emitted local <see cref="IAsyncInvocation{T}"/> variable. </param>
    /// <typeparam name="T"> The concrete type of the <see cref="IAsyncInvocation"/> feature. </typeparam>
    /// <remarks>
    /// Emits the following source code:
    /// <![CDATA[
    ///     T asyncFeature;
    /// ]]>
    /// </remarks>
    public static void EmitLocalAsyncFeatureVariable<T>(this ILGenerator body, out LocalBuilder asyncFeatureVariable)
        where T : IAsyncInvocation
    {
        asyncFeatureVariable = body.DeclareLocal(typeof(T));
    }

    /// <summary>
    /// Emits code to create a new <see cref="IAsyncInvocation{T}"/> instance for the given async feature.
    /// </summary>
    /// <param name="body"> The body of the dynamic method. </param>
    /// <param name="asyncFeatureVariable"> The emitted local <see cref="IAsyncInvocation{T}"/> variable. </param>
    /// <typeparam name="T"> The concrete type of the <see cref="IAsyncInvocation"/> feature. </typeparam>
    /// <remarks>
    /// Emits the following source code:
    /// <![CDATA[
    ///     asyncFeature = new Async...Invocation();
    /// ]]>
    /// </remarks>
    public static void EmitNewAsyncFeature<T>(this ILGenerator body, LocalBuilder asyncFeatureVariable)
        where T : IAsyncInvocation
    {
        Ensures.NotNull(asyncFeatureVariable, nameof(asyncFeatureVariable));

        var asyncFeature = AsyncFeatureCache.GetOrAdd(typeof(T), GetAsyncFeatureConstructor<T>());
        body.Emit(OpCodes.Newobj, asyncFeature);
        body.Emit(OpCodes.Stloc, asyncFeatureVariable.LocalIndex);
    }

    /// <summary>
    /// Emits code to return the <see cref="IAsyncInvocation{T}.AsyncReturnValue"/>.
    /// </summary>
    /// <param name="body"> The body of the dynamic method. </param>
    /// <param name="asyncFeatureVariable"> The emitted local <see cref="IAsyncInvocation{T}"/> variable. </param>
    /// <typeparam name="T"> The concrete type of the <see cref="IAsyncInvocation"/> feature. </typeparam>
    /// <remarks>
    /// Emits the following source code:
    /// <![CDATA[
    ///     return asyncFeature.AsyncReturnValue;
    /// ]]>
    /// </remarks>
    public static void EmitAsyncReturnStatement<T>(
        this ILGenerator body,
        LocalBuilder asyncFeatureVariable)
        where T : IAsyncInvocation
    {
        Ensures.NotNull(asyncFeatureVariable, nameof(asyncFeatureVariable));

        body.Emit(OpCodes.Ldloc, asyncFeatureVariable.LocalIndex);
        var asyncReturnValueSignature = AsyncFeatureReturnValueCache.GetOrAdd(typeof(T), GetAsyncFeatureReturnValue<T>());
        body.Emit(OpCodes.Callvirt, asyncReturnValueSignature);
        body.Emit(OpCodes.Ret);
    }

    /// <summary>
    /// Initialization logic for a <see cref="AsyncFeatureCache"/> item.
    /// </summary>
    /// <typeparam name="T"> The concrete type of the <see cref="IAsyncInvocation"/> feature. </typeparam>
    /// <returns> The signature of the feature's constructor. </returns>
    private static ConstructorInfo GetAsyncFeatureConstructor<T>()
        where T : IAsyncInvocation
    {
        var type = typeof(T);
        var constructor = type.GetConstructor(Array.Empty<Type>());
        return constructor ?? throw new ConstructorInfoException(type);
    }

    /// <summary>
    /// Initialization logic for a <see cref="AsyncFeatureReturnValueCache"/> item.
    /// </summary>
    /// <returns> The signature of the <see cref="IAsyncInvocation{T}.AsyncReturnValue"/> property's getter. </returns>
    private static MethodInfo GetAsyncFeatureReturnValue<T>()
        where T : IAsyncInvocation
    {
        var type = typeof(T);
        var propertyName = nameof(IAsyncInvocation<object>.AsyncReturnValue);
        var asyncReturnValueProperty = @type.GetProperty(propertyName);
        if (asyncReturnValueProperty == null)
        {
            throw new PropertyInfoException(type, propertyName);
        }

        var asyncReturnValueGetter = asyncReturnValueProperty.GetGetMethod();
        return asyncReturnValueGetter ?? throw new MethodInfoException(type, $"get_{propertyName}");
    }

    #endregion
}
