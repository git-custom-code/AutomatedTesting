namespace CustomCode.AutomatedTesting.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains a value type property getter.
    /// </summary>
    public interface IFooValueTypeGetter<T> : IFoo
        where T : struct
    {
        T Getter { get; }
    }
}