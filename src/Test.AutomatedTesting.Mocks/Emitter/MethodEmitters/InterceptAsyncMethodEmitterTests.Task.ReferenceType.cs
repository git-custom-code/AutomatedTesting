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
    [Fact(DisplayName = "MethodEmitter: Task (reference type) with single parameter")]
    public async Task TaskWithSingleReferenceTypeParameterAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncInterceptor();
        var expectedReferenceType = typeof(int);

        // When
        var foo = proxyFactory.CreateForInterface<IFooTaskReferenceTypeParameter>(interceptor);
        var task = foo.MethodWithOneParameterAsync(expectedReferenceType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);

        Assert.Single(interceptor.ForwardedInvocations);
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskReferenceTypeParameter.MethodWithOneParameterAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
    }

    [Fact(DisplayName = "MethodEmitter: Task (reference type) with overloaded method (first overload)")]
    public async Task TaskWithFirstOverloadedReferenceTypeMethodAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncInterceptor();
        var expectedReferenceType = typeof(int);

        // When
        var foo = proxyFactory.CreateForInterface<IFooTaskReferenceTypeOverloads>(interceptor);
        var task = foo.MethodWithOverloadAsync(expectedReferenceType);
        await task.ConfigureAwait(false);
        
        // Then
        Assert.NotNull(foo);
        
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskReferenceTypeOverloads.MethodWithOverloadAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveParameterInCountOf(1);
        invocation.ShouldHaveParameterIn("first", typeof(object), expectedReferenceType);
    }

    [Fact(DisplayName = "MethodEmitter: Task (reference type) with overloaded method (second overload)")]
    public async Task TaskWithSecondOverloadedReferenceTypeMethodAsync()
    {
        // Given
        var proxyFactory = CreateFactory();
        var interceptor = new AsyncInterceptor();
        var firstExpectedReferenceType = typeof(int);
        var secondExpectedReferenceType = typeof(string);

        // When
        var foo = proxyFactory.CreateForInterface<IFooTaskReferenceTypeOverloads>(interceptor);
        var task = foo.MethodWithOverloadAsync(firstExpectedReferenceType, secondExpectedReferenceType);
        await task.ConfigureAwait(false);

        // Then
        Assert.NotNull(foo);
        
        var invocation = interceptor.ForwardedInvocations.Single();
        invocation.ShouldInterceptMethodWithName(nameof(IFooTaskReferenceTypeOverloads.MethodWithOverloadAsync));
        invocation.ShouldBeAsyncInvocationOfType(AsyncInvocationType.Task);
        invocation.ShouldHaveParameterInCountOf(2);
        invocation.ShouldHaveParameterIn("first", typeof(object), firstExpectedReferenceType);
        invocation.ShouldHaveParameterIn("second", typeof(object), secondExpectedReferenceType);
    }
}
