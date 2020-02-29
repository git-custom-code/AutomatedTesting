namespace CustomCode.AutomatedTesting.Mocks.Interception.Internal
{
    using Fluent;
    using System;
    using System.Reflection;

    /// <summary>
    /// Implementation of an <see cref="IInterceptor"/> that is used to discover the called
    /// property setter defined within an <see cref="Action{T}"/> delegate.
    /// </summary>
    /// <remarks>
    /// This interceptor is used in <see cref="IMockBehavior{TMock}.ThatAssigning(Action{TMock})"/>.
    /// </remarks>
    public sealed class PropertySetterInterceptor : IInterceptor
    {
        #region Data

        /// <summary>
        /// Gets the signature of the discovered property setter.
        /// </summary>
        public MethodInfo? DiscoveredSetter { get; private set; }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void Intercept(IInvocation invocation)
        {
            if (invocation is SetterInvocation setter)
            {
                if (DiscoveredSetter == null)
                {
                    DiscoveredSetter = setter.Signature;
                }
                else
                {
                    throw new Exception("Discovered more than one property setter call");
                }
            }
        }

        #endregion
    }
}