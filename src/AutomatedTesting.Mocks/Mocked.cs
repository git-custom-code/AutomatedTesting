namespace CustomCode.AutomatedTesting.Mocks;

using Arrangements;
using Dependencies;
using Fluent;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Default implementation of the <see cref="IMocked{T}"/> interface.
/// </summary>
/// <typeparam name="T">
/// The signature of the type under test whose dependencies are replaced by mocks
/// (see <see cref="IMockedDependency"/> for more details).
/// </typeparam>
public sealed class Mocked<T> : IMocked<T> where T : class
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="Mocked{T}"/> type.
    /// </summary>
    /// <param name="instance"> An instance of the type under test whose dependencies are replaced by mocks. </param>
    /// <param name="dependencies"> The type's mocked dependencies. </param>
    public Mocked(T instance, IEnumerable<IMockedDependency> dependencies)
    {
        if (typeof(T).IsClass == false)
        {
            throw new ArgumentException($"{typeof(T).Name} must be a non-interface reference type", nameof(instance));
        }

        Instance = instance;
        Dependencies = dependencies;
        Arrangements = new ConcurrentDictionary<Type, IArrangementCollection>(dependencies.Select(
            d => new KeyValuePair<Type, IArrangementCollection>(d.Signature, d.Arrangements)));
    }

    #endregion

    #region Data

    /// <inheritdoc cref="IMocked{T}" />
    public T Instance { get; }

    /// <summary>
    /// Gets the type's mocked dependencies.
    /// </summary>
    private IEnumerable<IMockedDependency> Dependencies { get; }

    /// <summary>
    /// Gets a lookup table that contains arrangements for each mocked dependency.
    /// </summary>
    private ConcurrentDictionary<Type, IArrangementCollection> Arrangements { get; }

    #endregion

    #region Logic

    /// <inheritdoc cref="IMocked{T}" />
    public IMockBehavior<TMock> ArrangeFor<TMock>() where TMock : class
    {
        if (Arrangements.TryGetValue(typeof(TMock), out var mockArrangements))
        {
            return new MockBehavior<TMock>(mockArrangements);
        }

        throw new ArgumentException($"Type '{typeof(T).Name}' has no dependency of type '{typeof(TMock).Name}'");
    }

    #endregion
}
