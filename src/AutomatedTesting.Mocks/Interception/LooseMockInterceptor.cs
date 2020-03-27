namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using Arrangements;
    using CustomCode.AutomatedTesting.Mocks.Interception.Async;
    using Interception.ReturnValue;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of the <see cref="IInterceptor"/> interface for mocked dependency instances that will return
    /// default values for each intercepted method and property.
    /// </summary>
    public sealed class LooseMockInterceptor : IInterceptor
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="LooseMockInterceptor"/> type.
        /// </summary>
        /// <param name="arrangements"> A collection of <see cref="IArrangement"/>s for the intercepted calls. </param>
        public LooseMockInterceptor(IArrangementCollection arrangements)
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

        /// <summary>
        /// Gets the default value for a given <paramref name="type"/>.
        /// </summary>
        /// <param name="type"> The type whose default value should be returned. </param>
        /// <returns> The default value for the given <paramref name="type"/>. </returns>
        private object? GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        #endregion
    }
}