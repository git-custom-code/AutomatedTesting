namespace CustomCode.AutomatedTesting.Mocks.Tests.Extensions
{
    using Interception;
    using Xunit;

    /// <summary>
    /// Extension methods for the <see cref="IInvocation"/> type.
    /// </summary>
    public static partial class IInvocationExtensions
    {
        #region Logic

        /// <summary>
        /// Validates that the invocation intercepts a method with the given <paramref name="name"/>.
        /// </summary>
        /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
        /// <param name="name"> The expected method name. </param>
        public static void ShouldInterceptMethodWithName(this IInvocation invocation, string name)
        {
            Assert.Equal(name, invocation.Signature.Name);
        }

        #endregion
    }
}