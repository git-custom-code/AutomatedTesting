namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using Arrangements;
    using ExceptionHandling;
    using System;
    using System.Linq;

    /// <summary>
    /// Implementation of the <see cref="IInterceptor"/> interface for mocked dependency instances that will return
    /// default values for each intercepted method and property or call the original method if no arrangements have
    /// been made.
    /// </summary>
    public sealed class StrictMockInterceptor : IInterceptor
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="StrictMockInterceptor"/> type.
        /// </summary>
        /// <param name="arrangements"> A collection of <see cref="IArrangement"/>s for the intercepted calls. </param>
        public StrictMockInterceptor(IArrangementCollection arrangements)
        {
            Arrangements = arrangements ?? throw new ArgumentNullException(nameof(arrangements));
        }

        /// <summary>
        /// Gets a collection of <see cref="IArrangement"/>s for the intercepted calls.
        /// </summary>
        private IArrangementCollection Arrangements { get; }

        #endregion

        #region Logic

        /// <inheritdoc cref="IInterceptor" />
        public bool Intercept(IInvocation invocation)
        {
            if (Arrangements.TryApplyTo(invocation))
            {
                return true;
            }

            var type = invocation.Signature.DeclaringType;
            if (type != null)
            {
                var property = type.GetProperties().SingleOrDefault(p => p.GetSetMethod() == invocation.Signature);
                if (property != null)
                {
                    throw new MissingArrangementException(
                        $"No arrangements were made for mocked property setter '{type.Name}.{property.Name}'.",
                        invocation.Signature);
                }

                property = type.GetProperties().SingleOrDefault(p => p.GetGetMethod() == invocation.Signature);
                if (property != null)
                {
                    throw new MissingArrangementException(
                       $"No arrangements were made for mocked property getter '{type.Name}.{property.Name}'.",
                       invocation.Signature);
                }

                throw new MissingArrangementException(
                    $"No arrangements were made for mocked method '{type.Name}.{invocation.Signature.Name}'.",
                    invocation.Signature);
            }

            throw new MissingArrangementException(
                $"No arrangements were made for mocked method '{invocation.Signature.Name}'.",
                invocation.Signature);
        }

        #endregion
    }
}