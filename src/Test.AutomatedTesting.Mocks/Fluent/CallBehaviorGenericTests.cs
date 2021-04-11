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
        [Fact(DisplayName = "Setup a behavior to record one parameter of a called method")]
        public void SetupRecordOneParameterArrangement()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterIn<int>);
            var methodName = nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

            // When
            callBehavior.Records<int>(out var recordedCalls);

            // Then
            Assert.Single(arrangements);
            Assert.Empty(recordedCalls);
            var arrangement = arrangements.First() as RecordParameterArrangement<int>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to record two parameters of a called method")]
        public void SetupRecordTwoParametersArrangement()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterIn<int>);
            var methodName = nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

            // When
            callBehavior.Records<int, string?>(out var recordedCalls);

            // Then
            Assert.Single(arrangements);
            Assert.Empty(recordedCalls);
            var arrangement = arrangements.First() as RecordParameterArrangement<(int, string?)>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to record three parameters of a called method")]
        public void SetupRecordThreeParametersArrangement()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterIn<int>);
            var methodName = nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

            // When
            callBehavior.Records<int, string?, bool>(out var recordedCalls);

            // Then
            Assert.Single(arrangements);
            Assert.Empty(recordedCalls);
            var arrangement = arrangements.First() as RecordParameterArrangement<(int, string?, bool)>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to record four parameters of a called method")]
        public void SetupRecordFourParametersArrangement()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterIn<int>);
            var methodName = nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

            // When
            callBehavior.Records<int, int, int, int>(out var recordedCalls);

            // Then
            Assert.Single(arrangements);
            Assert.Empty(recordedCalls);
            var arrangement = arrangements.First() as RecordParameterArrangement<(int, int, int, int)>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to record five parameters of a called method")]
        public void SetupRecordFiveParametersArrangement()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterIn<int>);
            var methodName = nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

            // When
            callBehavior.Records<int, int, int, int, int>(out var recordedCalls);

            // Then
            Assert.Single(arrangements);
            Assert.Empty(recordedCalls);
            var arrangement = arrangements.First() as RecordParameterArrangement<(int, int, int, int, int)>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to record six parameters of a called method")]
        public void SetupRecordSixParametersArrangement()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterIn<int>);
            var methodName = nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

            // When
            callBehavior.Records<int, int, int, int, int, int>(out var recordedCalls);

            // Then
            Assert.Single(arrangements);
            Assert.Empty(recordedCalls);
            var arrangement = arrangements.First() as RecordParameterArrangement<(int, int, int, int, int, int)>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to record seven parameters of a called method")]
        public void SetupRecordSevenParametersArrangement()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterIn<int>);
            var methodName = nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

            // When
            callBehavior.Records<int, int, int, int, int, int, int>(out var recordedCalls);

            // Then
            Assert.Single(arrangements);
            Assert.Empty(recordedCalls);
            var arrangement = arrangements.First() as RecordParameterArrangement<(int, int, int, int, int, int, int)>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to record eight parameters of a called method")]
        public void SetupRecordEightParametersArrangement()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterIn<int>);
            var methodName = nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

            // When
            callBehavior.Records<int, int, int, int, int, int, int, int>(out var recordedCalls);

            // Then
            Assert.Single(arrangements);
            Assert.Empty(recordedCalls);
            var arrangement = arrangements.First() as RecordParameterArrangement<(int, int, int, int, int, int, int, int)>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to record nine parameters of a called method")]
        public void SetupRecordNineParametersArrangement()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterIn<int>);
            var methodName = nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

            // When
            callBehavior.Records<int, int, int, int, int, int, int, int, int>(out var recordedCalls);

            // Then
            Assert.Single(arrangements);
            Assert.Empty(recordedCalls);
            var arrangement = arrangements.First() as RecordParameterArrangement<(int, int, int, int, int, int, int, int, int)>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to return an out parameter when a method is called")]
        public void SetupReturnOutParameterArrangement()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterOut<int>);
            var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterOut<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterOut<int>>(arrangements, signature, mockBehavior);

            // When
            callBehavior.ReturnsOutParameterValue("first", 42);

            // Then
            Assert.Single(arrangements);
            var arrangement = arrangements.First() as OutParameterArrangement<int>;
            Assert.NotNull(arrangement);
        }

        [Fact(DisplayName = "Setup a behavior to return a single value for a called method")]
        public void SetupReturnSingleValueArrangement()
        {
            // Given
            var type = typeof(IFooFuncValueTypeParameterIn<int>);
            var methodName = nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>, int>(arrangements, signature, mockBehavior);

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
            var type = typeof(IFooFuncValueTypeParameterIn<int>);
            var methodName = nameof(IFooFuncValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>, int>(arrangements, signature, mockBehavior);

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
            var type = typeof(IFooFuncValueTypeParameterless<int>);
            var methodName = nameof(IFooFuncValueTypeParameterless<int>.MethodWithoutParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>, int>(arrangements, signature, mockBehavior);

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
            var type = typeof(IFooFuncValueTypeParameterless<int>);
            var methodName = nameof(IFooFuncValueTypeParameterless<int>.MethodWithoutParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var mockBehavior = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);
            var callBehavior = new CallBehavior<IFooFuncValueTypeParameterIn<int>, int>(arrangements, signature, mockBehavior);

            // When
            callBehavior.Throws(new Exception());

            // Then
            Assert.Single(arrangements);
            var arrangement = arrangements.First() as ExceptionArrangement;
            Assert.NotNull(arrangement);
        }
    }
}