namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using Arrangements;

    /// <summary>
    /// Implementation of the <see cref="IInterceptor"/> interface for mocked dependency instances that will return
    /// default values for each intercepted method and property.
    /// </summary>
    public sealed class PartialMockInterceptor : IInterceptor
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="LooseMockInterceptor"/> type.
        /// </summary>
        /// <param name="arrangements"> A collection of <see cref="IArrangement"/>s for the intercepted calls. </param>
        public PartialMockInterceptor(IArrangementCollection arrangements)
        {
            Arrangements = arrangements;
        }

        /// <summary>
        /// Gets a collection of <see cref="IArrangement"/>s for the intercepted calls.
        /// </summary>
        private IArrangementCollection Arrangements { get; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public bool Intercept(IInvocation invocation)
        {
            if (Arrangements.TryApplyTo(invocation))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}