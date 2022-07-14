namespace CustomCode.AutomatedTesting.Mocks.Interception.Tests;

using Arrangements;
using ExceptionHandling;
using ReturnValue;
using Xunit;

/// <summary>
/// Automated tests for the <see cref="LooseMockInterceptor"/> type.
/// </summary>
public sealed class LooseMockInterceptorTests
{
    [Fact(DisplayName = "MockBehavior.Loose: successfull invocation with arrangement")]
    public void SuccessfullInvocationWithArrangement()
    {
        // Given
        var type = typeof(object);
        var methodName = nameof(object.GetHashCode);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var methodInvocation = new Invocation(signature, new ReturnValueInvocation<int>());

        var arrangement = new ReturnValueArrangement<int>(signature, 42);
        var arrangements = new ArrangementCollection(new IArrangement[] { arrangement });
        var interceptor = new LooseMockInterceptor(arrangements);

        // When
        var wasIntercepted = interceptor.Intercept(methodInvocation);

        // Then
        Assert.True(wasIntercepted);
        var hasFeature = methodInvocation.TryGetFeature<IReturnValue<int>>(out var feature);
        Assert.True(hasFeature);
        Assert.Equal(42, feature?.ReturnValue);
    }

    [Fact(DisplayName = "MockBehavior.Loose: successfull invocation without arrangement")]
    public void SuccessfullInvocationWithoutArrangement()
    {
        // Given
        var type = typeof(object);
        var methodName = nameof(object.GetHashCode);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var methodInvocation = new Invocation(signature, new ReturnValueInvocation<int>());

        var emptyArrangements = new ArrangementCollection();
        var interceptor = new LooseMockInterceptor(emptyArrangements);

        // When
        var wasIntercepted = interceptor.Intercept(methodInvocation);

        // Then
        Assert.True(wasIntercepted);
        var hasFeature = methodInvocation.TryGetFeature<IReturnValue<int>>(out var feature);
        Assert.True(hasFeature);
        Assert.Equal(default(int), feature?.ReturnValue);
    }
}
