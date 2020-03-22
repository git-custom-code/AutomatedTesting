namespace CustomCode.AutomatedTesting.Mocks.Core.Extensions
{
    using Interception;
    using Interception.Properties;
    using System;
    using Xunit;

    /// <summary>
    /// Extension methods for the <see cref="IInvocation"/> type.
    /// </summary>
    public static partial class IInvocationExtensions
    {
        #region Logic

        /// <summary>
        /// Validates that the invocation intercepts a property with the given <paramref name="name"/>.
        /// </summary>
        /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
        /// <param name="name"> The expected property name. </param>
        public static void ShouldInterceptPropertyWithName(this IInvocation invocation, string name)
        {
            var feature = invocation.GetFeature<IPropertyInvocation>();
            Assert.Equal(name, feature.Signature.Name);
        }

        /// <summary>
        /// Validates that the invocation intercepts a property with the given value and type.
        /// </summary>
        /// <param name="invocation"> The extended <see cref="IInvocation"/> instance. </param>
        /// <param name="type"> The expected property value type. </param>
        /// <param name="value"> The expected property value. </param>
        public static void ShouldHavePropertyValue(this IInvocation invocation, Type type, object? value)
        {
            var feature = invocation.GetFeature<IPropertySetterValue>();
            Assert.Equal(type, feature.Type);
            Assert.Equal(value, feature.Value);
        }

        #endregion
    }
}