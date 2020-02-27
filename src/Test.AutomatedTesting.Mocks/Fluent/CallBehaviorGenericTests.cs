namespace CustomCode.AutomatedTesting.Mocks.Fluent.Tests
{
    using Arrangements;
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
            var arrangements = new ArrangementCollection();
            var signature = typeof(IBarWithValueTypeFunc)
                .GetMethod(nameof(IBarWithValueTypeFunc.MethodWithOneParameter))
                ?? throw new Exception("Unable to get requested signature via reflection");
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
            var arrangements = new ArrangementCollection();
            var signature = typeof(IBarWithValueTypeFunc)
                .GetMethod(nameof(IBarWithValueTypeFunc.MethodWithOneParameter))
                ?? throw new Exception("Unable to get requested signature via reflection");
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
            var arrangements = new ArrangementCollection();
            var signature = typeof(IBarWithValueTypeFunc)
                .GetMethod(nameof(IBarWithValueTypeFunc.MethodWithoutParameter))
                ?? throw new Exception("Unable to get requested signature via reflection");
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
            var arrangements = new ArrangementCollection();
            var signature = typeof(IBarWithValueTypeFunc)
                .GetMethod(nameof(IBarWithValueTypeFunc.MethodWithoutParameter))
                ?? throw new Exception("Unable to get requested signature via reflection");
            var callBehavior = new CallBehavior<int>(arrangements, signature);

            // When
            callBehavior.Throws(new ArgumentNullException());

            // Then
            Assert.Single(arrangements);
            var arrangement = arrangements.First() as ExceptionArrangement;
            Assert.NotNull(arrangement);
        }
    }
}