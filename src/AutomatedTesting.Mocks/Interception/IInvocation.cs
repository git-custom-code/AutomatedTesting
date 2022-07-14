namespace CustomCode.AutomatedTesting.Mocks.Interception;

using ExceptionHandling;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

/// <summary>
/// Interface for types that contain information of a method or property invocation
/// (e.g. the name of the invoked method, the passed parameter values, ...).
/// </summary>
public interface IInvocation
{
    /// <summary>
    /// Gets the signature of the invoked method or property.
    /// </summary>
    MethodInfo Signature { get; }

    /// <summary>
    /// Gets an incocation feature by type.
    /// </summary>
    /// <typeparam name="T"> The <see cref="IInvocationFeature"/>'s type. </typeparam>
    /// <returns> The requested feature. </returns>
    /// <exception cref="MissingFeatureException"> Thrown if the requested feature does not exist. </exception>
    T GetFeature<T>() where T : class, IInvocationFeature;

    /// <summary>
    /// Query if an invocation feature of the specified type exists.
    /// </summary>
    /// <typeparam name="T"> The <see cref="IInvocationFeature"/>'s type. </typeparam>
    /// <returns> True if the requested feature exists, false otherwise. </returns>
    bool HasFeature<T>() where T : class, IInvocationFeature;

    /// <summary>
    /// Try to get an incocation feature by type.
    /// </summary>
    /// <typeparam name="T"> The <see cref="IInvocationFeature"/>'s type. </typeparam>
    /// <param name="feature"> The requested feature or null if no such feature exists. </param>
    /// <returns> True if the requested <paramref name="feature"/> exists, false otherwise. </returns>
    bool TryGetFeature<T>([NotNullWhen(true)] out T? feature) where T : class, IInvocationFeature;
}
