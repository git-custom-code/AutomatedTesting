namespace CustomCode.AutomatedTesting.Mocks.Fluent.Tests
{
    using Arrangements;
    using ExceptionHandling;
    using System;
    using System.Linq;
    using TestDomain;
    using Xunit;

    /// <summary>
    /// Automated tests for the <see cref="CallBehavior"/> type.
    /// </summary>
    public sealed class CallBehaviorTests
    {
        [Fact(DisplayName = "Setup a behavior to record one parameter of a called method")]
        public void SetupRecordOneParameterArrangement()
        {
            // Given
            var type = typeof(IFooActionValueTypeParameterIn<int>);
            var methodName = nameof(IFooActionValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior(arrangements, signature);

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
            var type = typeof(IFooActionValueTypeParameterIn<int>);
            var methodName = nameof(IFooActionValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior(arrangements, signature);

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
            var type = typeof(IFooActionValueTypeParameterIn<int>);
            var methodName = nameof(IFooActionValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior(arrangements, signature);

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
            var type = typeof(IFooActionValueTypeParameterIn<int>);
            var methodName = nameof(IFooActionValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior(arrangements, signature);

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
            var type = typeof(IFooActionValueTypeParameterIn<int>);
            var methodName = nameof(IFooActionValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior(arrangements, signature);

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
            var type = typeof(IFooActionValueTypeParameterIn<int>);
            var methodName = nameof(IFooActionValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior(arrangements, signature);

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
            var type = typeof(IFooActionValueTypeParameterIn<int>);
            var methodName = nameof(IFooActionValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior(arrangements, signature);

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
            var type = typeof(IFooActionValueTypeParameterIn<int>);
            var methodName = nameof(IFooActionValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior(arrangements, signature);

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
            var type = typeof(IFooActionValueTypeParameterIn<int>);
            var methodName = nameof(IFooActionValueTypeParameterIn<int>.MethodWithOneParameter);
            var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
            var arrangements = new ArrangementCollection();
            var callBehavior = new CallBehavior(arrangements, signature);

            // When
            callBehavior.Records<int, int, int, int, int, int, int, int, int>(out var recordedCalls);

            // Then
            Assert.Single(arrangements);
            Assert.Empty(recordedCalls);
            var arrangement = arrangements.First() as RecordParameterArrangement<(int, int, int, int, int, int, int, int, int)>;
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
            var callBehavior = new CallBehavior(arrangements, signature);

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
            var callBehavior = new CallBehavior(arrangements, signature);

            // When
            callBehavior.Throws(new Exception());

            // Then
            Assert.Single(arrangements);
            var arrangement = arrangements.First() as ExceptionArrangement;
            Assert.NotNull(arrangement);
        }
    }
}