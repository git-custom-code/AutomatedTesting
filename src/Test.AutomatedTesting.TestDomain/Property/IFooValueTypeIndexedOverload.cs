namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a value type indexed property with an addional overload.
    /// </summary>
    public interface IFooValueTypeIndexedOverload<T> : IFoo
        where T : struct
    {
        T this[T first] { get; set; }

        T this[T first, T second] { get; set; }
    }
}