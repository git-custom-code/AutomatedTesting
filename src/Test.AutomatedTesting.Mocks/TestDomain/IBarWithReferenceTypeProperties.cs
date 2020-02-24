namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    /// <summary>
    /// Interface that simulates a dependency that contains value type properties.
    /// </summary>
    public interface IBarWithReferenceTypeProperties : IBar
    {
        object? Getter { get; }

        object? Setter { set; }

        object? GetterSetter { get; set; }
    }
}