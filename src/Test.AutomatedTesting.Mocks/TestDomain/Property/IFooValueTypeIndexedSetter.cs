namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a value type indexed property setter.
    /// </summary>
    public interface IFooValueTypeIndexedSetter<T> : IFoo
        where T : struct
    {
        T this[T i] { set; }
    }
}