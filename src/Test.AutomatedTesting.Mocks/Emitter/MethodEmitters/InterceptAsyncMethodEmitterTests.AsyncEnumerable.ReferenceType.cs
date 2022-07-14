namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Core.Extensions;
using Interception;
using Interception.Async;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDomain;
using Xunit;

#endregion

/// <summary>
/// Automated tests for the <see cref="InterceptAsyncMethodEmitter{T}"/> type.
/// </summary>
public sealed partial class InterceptAsyncMethodEmitterTests
{
    [Fact(DisplayName = "MethodEmitter: AsyncEnumerable (reference type) without parameters")]
    public async Task AsyncEnumerableReferenceTypeWithoutParametersAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncEnumerableReferenceTypeInterceptor();
        var results = new List<object?>();

        // When
        var foo = proxyFactory.CreateForInterface<IFooAsyncEnumerableReferenceTypeParameterless>(interceptor);
        var task = foo.MethodWithoutParameterAsync();
        await foreach(var result in task.ConfigureAwait(false))
        {
            results.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, results.Count);
        Assert.Equal(new[] { "foo", "foo", "foo" }, results);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableReferenceTypeParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "MethodEmitter: AsyncEnumerable (reference type) with single parameter")]
    public async Task AsyncEnumerableReferenceTypeWithSingleParameterAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncEnumerableReferenceTypeInterceptor();
        var expectedReferenceType = typeof(int);
        var results = new List<object?>();

        // When
        var foo = proxyFactory.CreateForInterface<IFooAsyncEnumerableReferenceTypeParameter>(interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedReferenceType);
        await foreach (var result in task.ConfigureAwait(false))
        {
            results.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, results.Count);
        Assert.Equal(new[] { "foo", "foo", "foo" }, results);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableReferenceTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
    }

    [Fact(DisplayName = "MethodEmitter: AsyncEnumerable (reference type) with overloaded method (first overload)")]
    public async Task AsyncEnumerableReferenceTypeWithFirstOverloadedMethodAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncEnumerableReferenceTypeInterceptor();
        var expectedReferenceType = typeof(int);
        var results = new List<object?>();
        
        // When
        var foo = proxyFactory.CreateForInterface<IFooAsyncEnumerableReferenceTypeOverloads>(interceptor);
        var task = foo.MethodWithOverloadAsync(expectedReferenceType);
        await foreach (var result in task.ConfigureAwait(false))
        {
            results.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, results.Count);
        Assert.Equal(new[] { "foo", "foo", "foo" }, results);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableReferenceTypeOverloads.MethodWithOverloadAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
    }

    [Fact(DisplayName = "MethodEmitter: AsyncEnumerable (reference type) with overloaded method (second overload)")]
    public async Task AsyncEnumerableReferenceTypeWithSecondOverloadedMethodAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncEnumerableReferenceTypeInterceptor();
        var firstExpectedReferenceType = typeof(int);
        var secondExpectedReferenceType = typeof(string);
        var results = new List<object?>();

        // When
        var foo = proxyFactory.CreateForInterface<IFooAsyncEnumerableReferenceTypeOverloads>(interceptor);
        var task = foo.MethodWithOverloadAsync(firstExpectedReferenceType, secondExpectedReferenceType);
        await foreach (var result in task.ConfigureAwait(false))
        {
            results.Add(result);
        }

        // Then
        Assert.NotNull(foo);
        Assert.Equal(3, results.Count);
        Assert.Equal(new[] { "foo", "foo", "foo" }, results);
       
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooAsyncEnumerableReferenceTypeOverloads.MethodWithOverloadAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.AsyncEnumerable);
        invocation.ShouldHaveParameterInCountOf(2);
        invocation.ShouldHaveParameterIn("first", typeof(object), firstExpectedReferenceType);
        invocation.ShouldHaveParameterIn("second", typeof(object), secondExpectedReferenceType);
    }

    #region Interceptor

    private sealed class AsyncEnumerableReferenceTypeInterceptor : IInterceptor
    {
        public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

        public bool Intercept(IInvocation invocation)
        {
            ForwardedInvocations.Add(invocation);
            if (invocation.TryGetFeature<IAsyncInvocation<IAsyncEnumerable<object?>>>(out var asyncFeature))
            {
                asyncFeature.AsyncReturnValue = AsyncEnumerable.Create(_ => new AsyncEnumeratorReferenceType());
                return true;
            }

            return false;
        }
    }

    private sealed class AsyncEnumeratorReferenceType : IAsyncEnumerator<object?>
    {
        public object? Current { get { return "foo"; } }

        private uint ElementCount { get; } = 3;

        private uint CurrentIndex { get; set; } = 0;

        public ValueTask DisposeAsync()
        {
            return default;
        }

        public ValueTask<bool> MoveNextAsync()
        {
            if (CurrentIndex < ElementCount)
            {
                ++CurrentIndex;
                return new ValueTask<bool>(true);
            }

            return new ValueTask<bool>(false);
        }
    }

    #endregion
}
