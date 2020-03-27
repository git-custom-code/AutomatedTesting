namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using Arrangements;
    using System;

    /// <summary>
    /// Default implementation of the <see cref="IInterceptorFactory"/> interface.
    /// </summary>
    public sealed class InterceptorFactory : IInterceptorFactory
    {
        #region Logic

        /// <inheritdoc />
        public IInterceptor CreateInterceptorFor(MockBehavior behavior, IArrangementCollection arrangements)
        {
            if (behavior == MockBehavior.Loose)
            {
                return new LooseMockInterceptor(arrangements);
            }

            if (behavior == MockBehavior.Partial)
            {
                return new PartialMockInterceptor(arrangements);
            }

            throw new NotSupportedException();
        }

        #endregion
    }
}