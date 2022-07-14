namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests;

#region Usings

using Core.Extensions;
using Interception.Async;
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
    [Fact(DisplayName = "MethodEmitter: Task (value type) with single parameter")]
    public async Task TaskWithSingleValueTypeParameterAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncInterceptor();
        var expectedValueType = 13;

        // When
        var foo = proxyFactory.CreateForInterface<IFooTaskValueTypeParameter>(interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedValueType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskValueTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
    }

    [Fact(DisplayName = "MethodEmitter: Task (value type) with overloaded method (first overload)")]
    public async Task TaskWithFirstOverloadedValueTypeMethodAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncInterceptor();
        var expectedValueType = 13;

        // When
        var foo = proxyFactory.CreateForInterface<IFooTaskValueTypeOverloads>(interceptor);
        var task = foo.MethodWithOverloadAsync(expectedValueType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskValueTypeOverloads.MethodWithOverloadAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(int), expectedValueType);
    }

    [Fact(DisplayName = "MethodEmitter: Task (value type) with overloaded method (second overload)")]
    public async Task TaskWithSecondOverloadedValueTypeMethodAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncInterceptor();
        var firstExpectedValueType = 13;
        var secondExpectedValueType = 42.0;

        // When
        var foo = proxyFactory.CreateForInterface<IFooTaskValueTypeOverloads>(interceptor);
        var task = foo.MethodWithOverloadAsync(firstExpectedValueType, secondExpectedValueType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);

        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskValueTypeOverloads.MethodWithOverloadAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveParameterInCountOf(2);
        invocation.ShouldHaveParameterIn("first", typeof(int), firstExpectedValueType);
        invocation.ShouldHaveParameterIn("second", typeof(double), secondExpectedValueType);
    }
}
