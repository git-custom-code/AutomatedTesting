namespace CustomCode.AutomatedTesting.Mocks.Arrangements;

using ExceptionHandling;
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
    public ArrangementCollection(IEnumerable<IArrangement>? arrangements)
    {
        if (arrangements != null)
        {
            Arrangements.AddRange(arrangements);
        }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ArrangementCollection"/> type.
    /// </summary>
    /// <param name="arrangements">
    /// An array of initial <see cref="IArrangement"/>s that should be stored within the collection.
    /// </param>
    public ArrangementCollection(params IArrangement[] arrangements)
    {
        if (arrangements != null)
        {
            Arrangements.AddRange(arrangements);
        }
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the internal <see cref="List{T}"/> that is used to store the <see cref="IArrangement"/> items.
    /// </summary>
    private List<IArrangement> Arrangements { get; } = new List<IArrangement>();

    /// <inheritdoc cref="IArrangementCollection" />
    public uint Count
    {
        get { return (uint)Arrangements.Count; }
    }

    #endregion

    #region Logic

    /// <inheritdoc cref="IArrangementCollection" />
    public void Add(IArrangement arrangement)
    {
        Ensures.NotNull(arrangement, nameof(arrangement));

        Arrangements.Add(arrangement);
    }

    /// <inheritdoc cref="IArrangementCollection" />
    public void ApplyTo(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        foreach (var arrangement in Arrangements)
        {
            arrangement.ApplyTo(invocation);
        }
    }

    /// <inheritdoc cref="IArrangementCollection" />
    public bool CanApplyAtLeasOneArrangmentTo(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        return Arrangements.Any(a => a.CanApplyTo(invocation));
    }

    /// <inheritdoc cref="IEnumerable{T}" />
    public IEnumerator<IArrangement> GetEnumerator()
    {
        foreach (var arrangement in Arrangements)
        {
            yield return arrangement;
        }
    }

    /// <inheritdoc cref="IEnumerable" />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc cref="object" />
    public override string ToString()
    {
        if (Arrangements.Count == 1)
        {
            return "1 Arrangement";
        }

        return $"{Arrangements.Count} Arrangements";
    }

    /// <inheritdoc cref="IArrangementCollection" />
    public bool TryApplyTo(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        var wasOneArrangementApplied = false;
        foreach (var arrangement in Arrangements)
        {
            wasOneArrangementApplied |= arrangement.TryApplyTo(invocation);
        }
        return wasOneArrangementApplied;
    }

    #endregion
}
