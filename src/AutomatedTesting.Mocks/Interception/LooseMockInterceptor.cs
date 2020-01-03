namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System;

    /// <summary>
    /// Implementation of the <see cref="IInterceptor"/> interface for mock instances that will return
    /// default values for each intercepted method and property.
    /// </summary>
    public sealed class LooseMockInterceptor : IInterceptor
    {
        #region Logic

        /// <inheritdoc />
        public void Intercept(IInvocation invocation)
        {
            throw new NotImplementedException("ToDo");
        }

        #endregion
    }
}