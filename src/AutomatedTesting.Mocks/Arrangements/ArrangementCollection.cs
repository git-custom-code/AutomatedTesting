namespace CustomCode.AutomatedTesting.Mocks.Arrangements
{
    using Interception;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Default implementation of the <see cref="IArrangementCollection"/> interface.
    /// </summary>
    public sealed class ArrangementCollection : IArrangementCollection
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ArrangementCollection"/> type.
        /// </summary>
        public ArrangementCollection()
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrangementCollection"/> type.
        /// </summary>
        /// <param name="arrangements">
        /// An array of initial <see cref="IArrangement"/>s that should be stored within the collection.
        /// </param>
        public ArrangementCollection(IEnumerable<IArrangement> arrangements)
        {
            Arrangements.AddRange(arrangements);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ArrangementCollection"/> type.
        /// </summary>
        /// <param name="arrangements">
        /// An array of initial <see cref="IArrangement"/>s that should be stored within the collection.
        /// </param>
        public ArrangementCollection(params IArrangement[] arrangements)
        {
            Arrangements.AddRange(arrangements);
        }

        #endregion

        #region Data

        /// <summary>
        /// Gets the internal <see cref="List{T}"/> that is used to store the <see cref="IArrangement"/> items.
        /// </summary>
        private List<IArrangement> Arrangements { get; } = new List<IArrangement>();

        /// <inheritdoc />
        public uint Count
        {
            get { return (uint)Arrangements.Count; }
        }

        #endregion

        #region Logic

        /// <inheritdoc />
        public void Add(IArrangement arrangement)
        {
            Arrangements.Add(arrangement);
        }

        /// <inheritdoc />
        public void ApplyTo(IInvocation invocation)
        {
            foreach(var arrangement in Arrangements)
            {
                arrangement.ApplyTo(invocation);
            }
        }

        /// <inheritdoc />
        public bool CanApplyAtLeasOneArrangmentTo(IInvocation invocation)
        {
            return Arrangements.Any(a => a.CanApplyTo(invocation));
        }

        /// <inheritdoc />
        public IEnumerator<IArrangement> GetEnumerator()
        {
            foreach (var arrangement in Arrangements)
            {
                yield return arrangement;
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (Arrangements.Count == 1)
            {
                return "1 Arrangement";
            }

            return $"{Arrangements.Count} Arrangements";
        }

        /// <inheritdoc />
        public bool TryApplyTo(IInvocation invocation)
        {
            var wasOneArrangementApplied = false;
            foreach (var arrangement in Arrangements)
            {
                wasOneArrangementApplied |= arrangement.TryApplyTo(invocation);
            }
            return wasOneArrangementApplied;
        }

        #endregion
    }
}