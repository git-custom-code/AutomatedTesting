namespace CustomCode.AutomatedTesting.Mocks.Interception.Parameters
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Record that contains the name, value and type of a single method's parameter.
    /// </summary>
    public sealed class Parameter : IEquatable<Parameter>
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="Parameter"/> type.
        /// </summary>
        /// <param name="name"> The parameter's name. </param>
        /// <param name="type"> The parameter's <see cref="System.Type"/>. </param>
        /// <param name="value"> The parameter's value. </param>
        public Parameter(string name, Type type, object? value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Value = value;
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the parameter's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the parameter's <see cref="System.Type"/>.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets or sets the parameter's value.
        /// </summary>
        public object? Value { get; set; }

        #endregion

        #region Logic

        /// <inheritdoc cref="object" />
        public static bool operator ==(Parameter left, Parameter right)
        {
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc cref="object" />
        public static bool operator !=(Parameter left, Parameter right)
        {
            return !string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc cref="object" />
        public override bool Equals(object? other)
        {
            if (other is Parameter parameter)
            {
                return string.Equals(Name, parameter.Name, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        /// <inheritdoc cref="IEquatable{T}" />
        public bool Equals([AllowNull] Parameter other)
        {
            return string.Equals(Name, other?.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc cref="object" />
        public override int GetHashCode()
        {
            return string.GetHashCode(Name, StringComparison.InvariantCulture);
        }

        /// <inheritdoc cref="object" />
        public override string ToString()
        {
            return $"{Name}: {Value} ({Type.Name})";
        }

        #endregion
    }
}