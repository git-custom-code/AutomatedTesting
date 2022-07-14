namespace CustomCode.AutomatedTesting.Mocks.Emitter.Extensions;

using ExceptionHandling;
using Interception.ReturnValue;
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
    /// Gets a thread-safe cache for <see cref="IReturnValue{T}"/> constructors.
    /// </summary>
    private static ConcurrentDictionary<Type, ConstructorInfo> ReturnValueFeatureCache { get; }
        = new ConcurrentDictionary<Type, ConstructorInfo>();

    /// <summary>
    /// Gets a thread-safe cache for <see cref="IReturnValue{T}.ReturnValue"/> getters.
    /// </summary>
    private static ConcurrentDictionary<Type, MethodInfo> ReturnValueFeaturePropertyCache { get; }
        = new ConcurrentDictionary<Type, MethodInfo>();

    #endregion

    #region Logic

    /// <summary>
    /// Emits a local return value feature variable of type <see cref="IReturnValue{T}"/>.
    /// </summary>
    /// <param name="body"> The body of the dynamic method. </param>
    /// <param name="returnValueFeatureVariable"> The emitted local <see cref="IReturnValue{T}"/> variable. </param>
    /// <typeparam name="T"> The concrete type of the return value. </typeparam>
    /// <remarks>
    /// Emits the following source code:
    /// <![CDATA[
    ///     IReturnValue<T> returnValueFeature;
    /// ]]>
    /// </remarks>
    public static void EmitLocalReturnValueFeatureVariable<T>(this ILGenerator body, out LocalBuilder returnValueFeatureVariable)
    {
        returnValueFeatureVariable = body.DeclareLocal(typeof(IReturnValue<T>));
    }

    /// <summary>
    /// Emits code to create a new <see cref="IReturnValue{T}"/> instance.
    /// </summary>
    /// <param name="body"> The body of the dynamic method. </param>
    /// <param name="returnValueFeatureVariable"> The emitted local <see cref="IReturnValue{T}"/> variable. </param>
    /// <typeparam name="T"> The concrete type of the return value. </typeparam>
    /// <remarks>
    /// Emits the following source code:
    /// <![CDATA[
    ///     returnValueFeature = new ReturnValueInvocation<T>();
    /// ]]>
    /// </remarks>
    public static void EmitNewReturnValueFeature<T>(this ILGenerator body, LocalBuilder returnValueFeatureVariable)
    {
        Ensures.NotNull(returnValueFeatureVariable, nameof(returnValueFeatureVariable));

        var returnValueFeature = ReturnValueFeatureCache.GetOrAdd(typeof(T), GetReturnValueFeatureConstructor<T>());
        body.Emit(OpCodes.Newobj, returnValueFeature);
        body.Emit(OpCodes.Stloc, returnValueFeatureVariable.LocalIndex);
    }

    /// <summary>
    /// Emits a single return statement for an intercepted void method.
    /// </summary>
    /// <param name="body"> The body of the dynamic method. </param>
    /// <remarks>
    /// Emits the following source code:
    /// <![CDATA[
    ///     return;
    /// ]]>
    /// </remarks>
    public static void EmitReturnStatement(this ILGenerator body)
    {
        body.Emit(OpCodes.Ret);
    }

    /// <summary>
    /// Emits code to return the <see cref="IReturnValue{T}.ReturnValue"/>.
    /// </summary>
    /// <param name="body"> The body of the dynamic method. </param>
    /// <param name="returnValueFeatureVariable"> The emitted local <see cref="IReturnValue{T}"/> variable. </param>
    /// <typeparam name="T"> The concrete type of the return value. </typeparam>
    /// <remarks>
    /// Emits the following source code:
    /// <![CDATA[
    ///     return returnValueFeature.ReturnValue;
    /// ]]>
    /// </remarks>
    public static void EmitReturnStatement<T>(
        this ILGenerator body,
        LocalBuilder returnValueFeatureVariable)
    {
        Ensures.NotNull(returnValueFeatureVariable, nameof(returnValueFeatureVariable));

        body.Emit(OpCodes.Ldloc, returnValueFeatureVariable.LocalIndex);
        var returnValueSignature = ReturnValueFeaturePropertyCache.GetOrAdd(typeof(T), GetReturnValue<T>());
        body.Emit(OpCodes.Callvirt, returnValueSignature);
        body.Emit(OpCodes.Ret);
    }

    /// <summary>
    /// Initialization logic for a <see cref="ReturnValueFeatureCache"/> item.
    /// </summary>
    /// <typeparam name="T"> The concrete type of the feature's return value. </typeparam>
    /// <returns> The signature of the feature's constructor. </returns>
    private static ConstructorInfo GetReturnValueFeatureConstructor<T>()
    {
        var type = typeof(ReturnValueInvocation<T>);
        var constructor = type.GetConstructor(Array.Empty<Type>());
        return constructor ?? throw new ConstructorInfoException(type);
    }

    /// <summary>
    /// Initialization logic for a <see cref="ReturnValueFeaturePropertyCache"/> item.
    /// </summary>
    /// <returns> The signature of the <see cref="IReturnValue{T}.ReturnValue"/> property's getter. </returns>
    private static MethodInfo GetReturnValue<T>()
    {
        var type = typeof(IReturnValue<T>);
        var propertyName = nameof(IReturnValue<T>.ReturnValue);
        var returnValueProperty = @type.GetProperty(propertyName);
        if (returnValueProperty == null)
        {
            throw new PropertyInfoException(type, propertyName);
        }

        var returnValueGetter = returnValueProperty.GetGetMethod();
        return returnValueGetter ?? throw new MethodInfoException(type, $"get_{propertyName}");
    }

    #endregion
}
