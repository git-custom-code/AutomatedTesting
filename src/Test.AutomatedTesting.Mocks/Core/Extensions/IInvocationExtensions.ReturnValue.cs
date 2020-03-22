namespace CustomCode.AutomatedTesting.Mocks.Core.Extensions
{
    using Interception;
    using Interception.ReturnValue;
    using Xunit;

    /// <summary>
    /// Extension methods for the <see cref="IInvocation"/> type.
    /// </summary>
    public static partial class IInvocationExtensions
    {
        #region Logic

        /// <summary>
        /// Validates that the invocation has a given return <paramref name="value"/>.
        /// </summary>
        /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
        /// <param name="value"> The invocation's return value. </param>
        public static void ShouldHaveReturnValue(this IInvocation invocation, object? value)
        {
            var feature = invocation.GetFeature<IReturnValue>();
            Assert.Equal(value, feature.ReturnValue);
        }

        #endregion
    }
}