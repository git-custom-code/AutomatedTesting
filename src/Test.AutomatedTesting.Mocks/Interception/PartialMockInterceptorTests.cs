namespace CustomCode.AutomatedTesting.Mocks.Interception.Tests
{
    using Arrangements;
    using ExceptionHandling;
    using ReturnValue;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="PartialMockInterceptor"/> type.
    /// </summary>
    public sealed class PartialMockInterceptorTests
    {
        [Fact(DisplayName = "MockBehavior.Partial: successfull invocation with arrangement")]
        public void SuccessfullInvocationWithArrangement()
        {
            // Given
            var type = typeof(object);
            var methodName = nameof(object.GetHashCode);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var methodInvocation = new Invocation(signature, new ReturnValueInvocation<int>());

            var arrangement = new ReturnValueArrangement<int>(signature, 42);
            var arrangements = new ArrangementCollection(new IArrangement[] { arrangement });
            var interceptor = new PartialMockInterceptor(arrangements);

            // When
            var wasIntercepted = interceptor.Intercept(methodInvocation);

            // Then
            Assert.True(wasIntercepted);
            var hasFeature = methodInvocation.TryGetFeature<IReturnValue<int>>(out var feature);
            Assert.True(hasFeature);
            Assert.Equal(42, feature?.ReturnValue);
        }

        [Fact(DisplayName = "MockBehavior.Partial: successfull invocation without arrangement")]
        public void SuccessfullInvocationWithoutArrangement()
        {
            // Given
            var type = typeof(object);
            var methodName = nameof(object.GetHashCode);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var methodInvocation = new Invocation(signature, new ReturnValueInvocation<int>());

            var emptyArrangements = new ArrangementCollection();
            var interceptor = new PartialMockInterceptor(emptyArrangements);

            // When
            var wasIntercepted = interceptor.Intercept(methodInvocation);

            // Then
            Assert.False(wasIntercepted);
        }
    }
}
