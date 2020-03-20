namespace CustomCode.AutomatedTesting.Mocks.TestDomain
{
    using System;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// Implementation of an <see cref="Exception"/> that can be used by <see cref="TheoryAttribute"/>s as data.
    /// </summary>
    public sealed class SerializableException : Exception, IXunitSerializable
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="SerializableException"/> type.
        /// </summary>
        public SerializableException()
            : base()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="SerializableException"/> type.
        /// </summary>
        /// <param name="message"> The exception's error message. </param>
        public SerializableException(string message)
            : base(message)
        { }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void Deserialize(IXunitSerializationInfo info)
        {
            //Message = info.GetValue<string>(nameof(SerializableException));
        }

        /// <inheritdoc />
        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue(nameof(SerializableException), Message);
        }

        #endregion
    }
}