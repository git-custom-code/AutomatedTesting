namespace CustomCode.AutomatedTesting.Mocks.Interception.ReturnValue
{
    using System;
    using System.Globalization;
 
    /// <summary>
    /// Implementation of an <see cref="IInvocationFeature"/> for invocations of methods
    /// that return a non-void return value.
    /// </summary>
    public sealed class ReturnValueInvocation<T> : IReturnValue<T>
    {
        #region Data

        /// <inheritdoc />
#pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
        public T ReturnValue { get; set; } = default;
#pragma warning restore CS8653

        /// <inheritdoc />
        object? IReturnValue.ReturnValue
        {
            get { return (object?)ReturnValue; }
#pragma warning disable CS8601 // Possible null reference assignment.
            set { ReturnValue = (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture); }
#pragma warning restore CS8601
        }

        #endregion
    }
}