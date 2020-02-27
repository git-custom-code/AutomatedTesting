namespace CustomCode.AutomatedTesting.Mocks.Interception
{
    using System;
    using System.Collections.Generic;
 
    /// <summary>
    /// Feature interface for <see cref="IInvocation"/>s of methods or properties that have input parameters.
    /// </summary>
    public interface IHasInputParameter
    {
        /// <summary>
        /// Gets the parameter signatures and passed values of the invoked method or property.
        /// </summary>
        public IEnumerable<(Type type, object? value)> InputParameter { get; }
    }
}