namespace CustomCode.AutomatedTesting.Mocks.Interception.ReturnValue
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
 
    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of methods
    /// that return a non-void return value.
    /// </summary>
    public sealed class ReturnValueInvocation<T> : IReturnValue<T>
    {
        #region Data

        /// <inheritdoc cref="IReturnValue{T}" />
        [AllowNull, MaybeNull]
#nullable disable
        public T ReturnValue { get; set; } = default;
#nullable restore

        /// <inheritdoc cref="IReturnValue" />
        object? IReturnValue.ReturnValue
        {
            get { return (object?)ReturnValue; }
#nullable disable
            set { ReturnValue = (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture); }
#nullable restore
        }

        #endregion
    }
}