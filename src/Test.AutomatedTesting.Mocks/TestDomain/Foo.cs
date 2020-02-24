namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Implementation of the <see cref="IFoo{T}"/> interface that simulates a type with a single dependency.
    /// </summary>
    public sealed class Foo<T> where T : IBar
    {
        public Foo(T dependency)
        {
            Dependency = dependency;
        }

        public T Dependency { get; }
    }
}