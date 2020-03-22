namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a value type indexed property getter.
    /// </summary>
    public interface IFooValueTypeIndexedGetter<T> : IFoo
        where T : struct
    {
        T this[T i] { get; }
    }
}