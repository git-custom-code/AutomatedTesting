namespace CustomCode.AutomatedTesting.Mocks.Arrangements;

using ExceptionHandling;
using Interception;
using Interception.Async;
using Interception.ReturnValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

/// <summary>
/// Arrange a sequence of custom return values for intercepted method or property calls.
/// </summary>
/// <typeparam name="T"> The type of the arranged return value sequence. </typeparam>
public sealed class ReturnValueSequenceArrangement<T> : IArrangement
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="ReturnValueSequenceArrangement{T}"/> type.
    /// </summary>
    /// <param name="signature">
    /// The signature of the intercepted method or property that is the target for this arrangement.
    /// </param>
    /// <param name="returnValueSequence"> The sequence of arranged return values. </param>
    public ReturnValueSequenceArrangement(MethodInfo signature, IList<T> returnValueSequence)
    {
        Signature = signature ?? throw new ArgumentNullException(nameof(signature));
        ReturnValueSequence = returnValueSequence ?? new List<T>();
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the sequence of arranged return values.
    /// </summary>
    private IList<T> ReturnValueSequence { get; }

    /// <summary>
    /// Gets the <see cref="ReturnValueSequence"/> index for the next value to be returned.
    /// </summary>
    private int SequenceIndex { get; set; } = 0;

    /// <summary>
    /// Gets the signature of the intercepted method or property that is the target for this arrangement.
    /// </summary>
    private MethodInfo Signature { get; }

    /// <summary>
    /// Gets a light-weight synchronization object for thread-safety.
    /// </summary>
    private object SyncLock { get; } = new object();

    #endregion

    #region Logic

    /// <inheritdoc cref="IArrangement" />
    public void ApplyTo(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        TryApplyTo(invocation);
    }

    /// <inheritdoc cref="IArrangement" />
    public bool CanApplyTo(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        if (invocation.HasFeature<IReturnValue<T>>())
        {
            return invocation.Signature == Signature;
        }

        return false;
    }

    /// <inheritdoc cref="object" />
    public override string ToString()
    {
        return $"Calls to '{Signature.Name}' should return '{ReturnValueSequence[SequenceIndex]}'";
    }

    /// <inheritdoc cref="IArrangement" />
    public bool TryApplyTo(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        if (invocation.Signature == Signature)
        {
            if (invocation.HasFeature<IAsyncInvocation>())
            {
                if (invocation.TryGetFeature<IAsyncInvocation<T>>(out var asyncFeature))
                {
                    asyncFeature.AsyncReturnValue = GetNextReturnValue();
                    return true;
                }
                else if (invocation.TryGetFeature<IAsyncInvocation<Task<T>>>(out var asyncTaskFeature))
                {
                    var returnValue = GetNextReturnValue();
                    asyncTaskFeature.AsyncReturnValue = Task.FromResult(returnValue);
                    return true;
                }
                else if (invocation.TryGetFeature<IAsyncInvocation<ValueTask<T>>>(out var asyncValueTaskFeature))
                {
                    var returnValue = GetNextReturnValue();
                    asyncValueTaskFeature.AsyncReturnValue = new ValueTask<T>(returnValue);
                    return true;
                }
                else if (invocation.TryGetFeature<IAsyncInvocation<IAsyncEnumerable<T>>>(out var asyncEnumerableFeature))
                {
                    var returnValue = GetNextReturnValue();
                    asyncEnumerableFeature.AsyncReturnValue = AsyncEnumerable.Create(_ =>
                        AsyncEnumerator.Create(
                            () => new ValueTask<bool>(true),
                            () => returnValue,
                            () => default));
                    return true;
                }
            }
            else if (invocation.TryGetFeature<IReturnValue<T>>(out var returnValueFeature))
            {
                returnValueFeature.ReturnValue = GetNextReturnValue();
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Gets the next return value from the <see cref="ReturnValueSequence"/> in a thread-safe way.
    /// </summary>
    /// <returns> The next return value. </returns>
    private T GetNextReturnValue()
    {
        var returnValue = ReturnValueSequence[SequenceIndex];
        if (SequenceIndex < ReturnValueSequence.Count - 1)
        {
            lock (SyncLock)
            {
                if (SequenceIndex < ReturnValueSequence.Count - 1)
                {
                    SequenceIndex++;
                }
            }
        }
        return returnValue;
    }

    #endregion
}
