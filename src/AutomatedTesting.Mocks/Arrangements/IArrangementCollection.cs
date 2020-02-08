namespace CustomCode.AutomatedTesting.Mocks.Arrangements
{
    using Interception;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for a custom collection of <see cref="IArrangement"/>s.
    /// </summary>
    public interface IArrangementCollection : IEnumerable<IArrangement>
    {
        /// <summary>
        /// Gets the number of arrangements stored in the collection.
        /// </summary>
        uint Count { get; }

        /// <summary>
        /// Add a new arrangement to the collection.
        /// </summary>
        /// <param name="arrangement"> The arrangement to be added. </param>
        void Add(IArrangement arrangement);

        /// <summary>
        /// Applies the collection's arrangements to an intercepted <paramref name="invocation"/> if the signatures match.
        /// </summary>
        /// <param name="invocation"> The intercepted method or property invocation. </param>
        void ApplyTo(IInvocation invocation);

        /// <summary>
        /// Query if at least one signature of an arrangment matches with the signature of the <paramref name="invocation"/>.
        /// </summary>
        /// <param name="invocation"> The target incovation. </param>
        /// <returns> True if at least one arrangment can be applied to the incovation, false otherwise. </returns>
        bool CanApplyAtLeasOneArrangmentTo(IInvocation invocation);

        /// <summary>
        /// Try to apply the colleciton's arrangements to an intercepted <paramref name="invocation"/> if the signatures match.
        /// </summary>
        /// <param name="invocation"> The intercepted method or property invocation. </param>
        /// <returns> True if at least one arrangment was applied to the incovation, false otherwise. </returns>
        bool TryApplyTo(IInvocation invocation);
    }
}