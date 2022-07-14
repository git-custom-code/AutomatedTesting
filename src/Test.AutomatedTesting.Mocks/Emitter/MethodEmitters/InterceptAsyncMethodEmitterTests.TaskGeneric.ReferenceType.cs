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
    [Fact(DisplayName = "MethodEmitter: GenericTask (reference type) without parameters")]
    public async Task GenericTaskWithoutReferenceTypeParametersAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncGenericReferenceTypeTaskInterceptor();

        // When
        var foo = proxyFactory.CreateForInterface<IFooGenericTaskReferenceTypeParameterless>(interceptor);
        var task = foo.MethodWithoutParameterAsync();
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal("foo", result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskReferenceTypeParameterless.MethodWithoutParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveNoParameterIn();
    }

    [Fact(DisplayName = "MethodEmitter: GenericTask (reference type) with single parameter")]
    public async Task GenericTaskWithSingleReferenceTypeParameterAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncGenericReferenceTypeTaskInterceptor();
        var expectedReferenceType = typeof(int);

        // When
        var foo = proxyFactory.CreateForInterface<IFooGenericTaskReferenceTypeParameter>(interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedReferenceType);
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal("foo", result);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskReferenceTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
    }

    [Fact(DisplayName = "MethodEmitter: GenericTask (reference type) with overloaded method (first overload)")]
    public async Task GenericTaskWithFirstOverloadedReferenceTypeMethodAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncGenericReferenceTypeTaskInterceptor();
        var expectedReferenceType = typeof(int);

        // When
        var foo = proxyFactory.CreateForInterface<IFooGenericTaskReferenceTypeOverloads>(interceptor);
        var task = foo.MethodWithOverloadAsync(expectedReferenceType);
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal("foo", result);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskReferenceTypeOverloads.MethodWithOverloadAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
    }

    [Fact(DisplayName = "MethodEmitter: GenericTask (reference type) with overloaded method (second overload)")]
    public async Task GenericTaskWithSecondOverloadedReferenceTypeMethodAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncGenericReferenceTypeTaskInterceptor();
        var firstExpectedReferenceType = typeof(int);
        var secondExpectedReferenceType = typeof(string);

        // When
        var foo = proxyFactory.CreateForInterface<IFooGenericTaskReferenceTypeOverloads>(interceptor);
        var task = foo.MethodWithOverloadAsync(firstExpectedReferenceType, secondExpectedReferenceType);
        var result = await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        Assert.Equal("foo", result);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooGenericTaskReferenceTypeOverloads.MethodWithOverloadAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.GenericTask);
        invocation.ShouldHaveParameterInCountOf(2);
        invocation.ShouldHaveParameterIn("first", typeof(object), firstExpectedReferenceType);
        invocation.ShouldHaveParameterIn("second", typeof(object), secondExpectedReferenceType);
    }

    #region Interceptor

    private sealed class AsyncGenericReferenceTypeTaskInterceptor : IInterceptor
    {
        public List<IInvocation> ForwardedInvocations { get; } = new List<IInvocation>();

        public bool Intercept(IInvocation invocation)
        {
            ForwardedInvocations.Add(invocation);
            if (invocation.TryGetFeature<IAsyncInvocation<Task<object?>>>(out var asyncFeature))
            {
                asyncFeature.AsyncReturnValue = Task.FromResult<object?>("foo");
                return true;
            }

            return false;
        }
    }

    #endregion
}
