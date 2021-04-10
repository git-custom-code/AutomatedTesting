namespace CustomCode.AutomatedTesting.Mocks.Interception.Tests
{
    using Arrangements;
    using ExceptionHandling;
    using ReturnValue;
    using System.IO;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="StrictMockInterceptor"/> type.
    /// </summary>
    public sealed class StrictMockInterceptorTests
    {
        [Fact(DisplayName = "MockBehavior.Strict: method invocation without arrangement throws exception")]
        public void ExcpetionIsThrownForUnarrangeMethodInvocation()
        {
            // Given
            var type = typeof(object);
            var methodName = nameof(object.GetHashCode);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var methodInvocation = new Invocation(signature);

            var emptyArrangements = new ArrangementCollection();
            var interceptor = new StrictMockInterceptor(emptyArrangements);

            // When
            var exception = Assert.Throws<MissingArrangementException>(() => interceptor.Intercept(methodInvocation));

            // Then
            Assert.Contains(type.Name, exception.Message);
            Assert.Contains(methodName, exception.Message);
            Assert.Contains("method", exception.Message);
        }

        [Fact(DisplayName = "MockBehavior.Strict: getter invocation without arrangement throws exception")]
        public void ExcpetionIsThrownForUnarrangePropertyGetterInvocation()
        {
            // Given
            var type = typeof(Stream);
            var propertyName = nameof(Stream.Position);
            var signature = type.GetProperty(propertyName)?.GetGetMethod() ?? throw new PropertyInfoException(type, propertyName);
            var getterInvocation = new Invocation(signature);

            var emptyArrangements = new ArrangementCollection();
            var interceptor = new StrictMockInterceptor(emptyArrangements);

            // When
            var exception = Assert.Throws<MissingArrangementException>(() => interceptor.Intercept(getterInvocation));

            // Then
            Assert.Contains(type.Name, exception.Message);
            Assert.Contains(propertyName, exception.Message);
            Assert.Contains("getter", exception.Message);
        }

        [Fact(DisplayName = "MockBehavior.Strict: setter invocation without arrangement throws exception")]
        public void ExcpetionIsThrownForUnarrangePropertySetterInvocation()
        {
            // Given
            var type = typeof(Stream);
            var propertyName = nameof(Stream.Position);
            var signature = type.GetProperty(propertyName)?.GetSetMethod() ?? throw new PropertyInfoException(type, propertyName);
            var setterInvocation = new Invocation(signature);

            var emptyArrangements = new ArrangementCollection();
            var interceptor = new StrictMockInterceptor(emptyArrangements);

            // When
            var exception = Assert.Throws<MissingArrangementException>(() => interceptor.Intercept(setterInvocation));

            // Then
            Assert.Contains(type.Name, exception.Message);
            Assert.Contains(propertyName, exception.Message);
            Assert.Contains("setter", exception.Message);
        }

        [Fact(DisplayName = "MockBehavior.Strict: successfull invocation with arrangement")]
        public void SuccessfullInvocationWithArrangement()
        {
            // Given
            var type = typeof(object);
            var methodName = nameof(object.GetHashCode);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var methodInvocation = new Invocation(signature, new ReturnValueInvocation<int>());

            var arrangement = new ReturnValueArrangement<int>(signature, 42);
            var arrangements = new ArrangementCollection(new IArrangement[] { arrangement });
            var interceptor = new StrictMockInterceptor(arrangements);

            // When
            var wasIntercepted = interceptor.Intercept(methodInvocation);

            // Then
            Assert.True(wasIntercepted);
            var hasFeature = methodInvocation.TryGetFeature<IReturnValue<int>>(out var feature);
            Assert.True(hasFeature);
            Assert.Equal(42, feature?.ReturnValue);
        }
    }
}
