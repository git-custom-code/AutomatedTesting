namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System.Threading.Tasks;

    /// <summary>
    /// Feature interface for <see cref="IInvocation"/>s of asynhrononous methods that have a return value.
    /// </summary>
    public interface IHasAsyncReturnValue
    {
        /// <summary>
        /// Gets or sets the asynchronous return value of the intercepted method.
        /// </summary>
        Task ReturnValue { get; set; }
    }
}