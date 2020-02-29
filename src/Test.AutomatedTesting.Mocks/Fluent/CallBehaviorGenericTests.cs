namespace CustomCode.AutomatedTesting.Mocks.Fluent.Tests
{
    using Arrangements;
    using ExceptionHandling;
    using System;
    using System.Linq;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="CallBehavior{T}"/> type.
    /// </summary>
    public sealed class CallBehaviorGenericTests
    {
        [Fact(DisplayName = "Setup a behavior to return a single value for a called method")]
        public void SetupReturnSingleValueArrangement()
        {
            // Given
            var type = typeof(IFooWithValueTypeAction);
            var methodName = nameof(IFooWithValueTypeAction.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior<int>(arrangements, signature);

            // When
            callBehavior.Returns(42);

            // Then
            Assert.Single(arrangements);
            var arrangement = arrangements.First() as ReturnValueArrangement<int>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to return a sequence of values for a called method")]
        public void SetupReturnSequenceArrangement()
        {
            // Given
            var type = typeof(IFooWithValueTypeAction);
            var methodName = nameof(IFooWithValueTypeAction.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior<int>(arrangements, signature);

            // When
            callBehavior.ReturnsSequence(13, 42);

            // Then
            Assert.Single(arrangements);
            var arrangement = arrangements.First() as ReturnValueSequenceArrangement<int>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to throw an exception when a method is called")]
        public void SetupThrowExceptionArrangement()
        {
            // Given
            var type = typeof(IFooWithValueTypeAction);
            var methodName = nameof(IFooWithValueTypeAction.MethodWithoutParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior<int>(arrangements, signature);

            // When
            callBehavior.Throws<ArgumentException>();

            // Then
            Assert.Single(arrangements);
            var arrangement = arrangements.First() as ExceptionArrangement;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to rethrow an exception instance when a method is called")]
        public void SetupRethrowExceptionInstanceArrangement()
        {
            // Given
            var type = typeof(IFooWithValueTypeAction);
            var methodName = nameof(IFooWithValueTypeAction.MethodWithoutParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior<int>(arrangements, signature);

            // When
            callBehavior.Throws(new Exception());

            // Then
            Assert.Single(arrangements);
            var arrangement = arrangements.First() as ExceptionArrangement;
            Assert.NotNull(arrangement);
        }
    }
}