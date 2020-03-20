namespace CustomCode.AutomatedTesting.Mocks.Core.Extensions
{
    using Interception;
    using Interception.Parameters;
    using System;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Extension methods for the <see cref="IInvocation"/> type.
    /// </summary>
    public static partial class IInvocationExtensions
    {
        #region Logic

        /// <summary>
        /// Validates that the invocation has no input parameters.
        /// </summary>
        /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
        public static void ShouldHaveNoParameterIn(this IInvocation invocation)
        {
            Assert.False(invocation.HasFeature<IParameterIn>());
        }

        /// <summary>
        /// Validates that the invocation has <paramref name="count"/> input parameters.
        /// </summary>
        /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
        /// <param name="count"> The number of input parameter to be validated. </param>
        public static void ShouldHaveParameterInCountOf(this IInvocation invocation, int count)
        {
            var feature = invocation.GetFeature<IParameterIn>();
            Assert.Equal(count, feature.InputParameterCollection.Count());
        }

        /// <summary>
        /// Validates that the invocation has an input parameter with the given name, type and value.
        /// </summary>
        /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
        /// <param name="name"> The unique name of the input parameter to be validated. </param>
        /// <param name="type"> The type of the input parameter to be validated. </param>
        /// <param name="value"> The value of the input parameter to be validated. </param>
        public static void ShouldHaveParameterIn(this IInvocation invocation, string name, Type type, object? value)
        {
            var feature = invocation.GetFeature<IParameterIn>();
            var parameter = feature.InputParameterCollection.SingleOrDefault(
                p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
            Assert.Equal(name, parameter.Name);
            Assert.Equal(type, parameter.Type);
            Assert.Equal(value, parameter.Value);
        }

        #endregion
    }
}