namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    /// <summary>
    /// Feature interface for <see cref="IInvocation"/>s of methods or properties that have a return value.
    /// </summary>
    public interface IHasReturnValue
    {
        /// <summary>
        /// Gets or sets the return value of the intercepted method or property getter.
        /// </summary>
        object? ReturnValue { get; set; }
    }
}