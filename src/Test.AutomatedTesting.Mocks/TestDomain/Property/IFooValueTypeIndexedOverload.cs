namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a value type indexed property with an addional overload.
    /// </summary>
    public interface IFooValueTypeIndexedOverload<T> : IFoo
        where T : struct
    {
        T this[T i] { get; set; }

        T this[T i, T j] { get; set; }
    }
}