namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Interface that simulates a type with a single dependency.
/// </summary>
public interface IBar<T> where T : IFoo
{
    T Dependency { get; }
}
