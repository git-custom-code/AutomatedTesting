namespace CustomCode.AutomatedTesting.Mocks.Dependencies;

using System;

/// <summary>
/// Interface for a factory that can created <see cref="IMockedDependency{T}"/> instances for a given
/// dependency (whose signature is defined by its interface type).
/// </summary>
public interface IMockedDependencyFactory
{
    /// <summary>
    /// Creates a new decorated <see cref="IMockedDependency"/> for a given <paramref name="dependency"/>
    /// with the specified <paramref name="decoratee"/> instance that will be called when no arrangements
    /// have been setup.
    /// </summary>
    /// <param name="dependency">
    /// The signature of the dependency that should be mocked (must be an interface).
    /// </param>
    /// <param name="decoratee">
    /// The instance that will be called when no arrangements have been setup.
    /// </param>
    /// <returns> The mocked dependency. </returns>
    public IMockedDependency CreateDecoratedDependency(Type dependency, object decoratee);

    /// <summary>
    /// Creates a new decorated <see cref="IMockedDependency"/> for a given dependency with the
    /// specified <paramref name="decoratee"/> instance that will be called when no arrangements
    /// have been setup.
    /// </summary>
    /// <typeparam name="T">
    /// The signature of the dependency that should be mocked (must be an interface).
    /// </typeparam>
    /// <param name="decoratee">
    /// The instance that will be called when no arrangements have been setup.
    /// </param>
    /// <returns> The mocked dependency. </returns>
    IMockedDependency<T> CreateDecoratedDependency<T>(T decoratee) where T : notnull;

    /// <summary>
    /// Creates a new <see cref="IMockedDependency"/> for a given <paramref name="dependency"/> with the
    /// specified <paramref name="behavior"/>.
    /// </summary>
    /// <param name="dependency">
    /// The signature of the dependency that should be mocked (must be an interface).
    /// </param>
    /// <param name="behavior">
    /// The behavior of the mocked dependency (see <see cref="MockBehavior"/> for more details).
    /// </param>
    /// <returns> The mocked dependency. </returns>
    IMockedDependency CreateMockedDependency(Type dependency, MockBehavior behavior);

    /// <summary>
    /// Creates a new <see cref="IMockedDependency"/> for a given dependency with the
    /// specified <paramref name="behavior"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The signature of the dependency that should be mocked (must be an interface).
    /// </typeparam>
    /// <param name="behavior">
    /// The behavior of the mocked dependency (see <see cref="MockBehavior"/> for more details).
    /// </param>
    /// <returns> The mocked dependency. </returns>
    IMockedDependency<T> CreateMockedDependency<T>(MockBehavior behavior) where T : notnull;
}
