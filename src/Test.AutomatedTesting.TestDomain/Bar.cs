namespace CustomCode.AutomatedTesting.TestDomain;

/// <summary>
/// Implementation of the <see cref="IBar{T}"/> interface that simulates a type with a single dependency.
/// </summary>
public sealed class Bar<T> where T : IFoo
{
    public Bar(T dependency)
    {
        Dependency = dependency;
    }

    public T Dependency { get; }
}
