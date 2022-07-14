namespace CustomCode.AutomatedTesting.Mocks.Fluent.Tests;

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
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterIn<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

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
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterIn<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

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
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterIn<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

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
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterIn<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

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
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterIn<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

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
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterIn<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

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
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterIn<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

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
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterIn<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

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
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterIn<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterIn<int>>(arrangements, signature, mockBehavior);

        // When
        callBehavior.Records<int, int, int, int, int, int, int, int, int>(out var recordedCalls);

        // Then
        Assert.Single(arrangements);
        Assert.Empty(recordedCalls);
        var arrangement = arrangements.First() as RecordParameterArrangement<(int, int, int, int, int, int, int, int, int)>;
        Assert.NotNull(arrangement);
    }

    [Fact(DisplayName = "Setup a behavior to return an out parameter when a method is called")]
    public void SetupReturnsOutParameterArrangement()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var arrangements = new ArrangementCollection();
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterOut<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterOut<int>>(arrangements, signature, mockBehavior);

        // When
        callBehavior.ReturnsOutParameterValue("first", 42);

        // Then
        Assert.Single(arrangements);
        var arrangement = arrangements.First() as OutParameterArrangement<int>;
        Assert.NotNull(arrangement);
    }

    [Fact(DisplayName = "Setup a behavior to return a sequence of out parameter values when a method is called")]
    public void SetupReturnsOutParameterSequenceArrangement()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var arrangements = new ArrangementCollection();
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterOut<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterOut<int>>(arrangements, signature, mockBehavior);

        // When
        callBehavior.ReturnsOutParameterSequence("first", 13, 42, 65);

        // Then
        Assert.Single(arrangements);
        var arrangement = arrangements.First() as OutParameterSequenceArrangement<int>;
        Assert.NotNull(arrangement);
    }

    [Fact(DisplayName = "Setup a behavior to return a ref parameter when a method is called")]
    public void SetupReturnsRefParameterArrangement()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterRef<int>);
        var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var arrangements = new ArrangementCollection();
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterRef<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterRef<int>>(arrangements, signature, mockBehavior);

        // When
        callBehavior.ReturnsRefParameterValue("first", 42);

        // Then
        Assert.Single(arrangements);
        var arrangement = arrangements.First() as RefParameterArrangement<int>;
        Assert.NotNull(arrangement);
    }

    [Fact(DisplayName = "Setup a behavior to return a sequence of ref parameter values when a method is called")]
    public void SetupReturnsRefParameterSequenceArrangement()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterRef<int>);
        var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var arrangements = new ArrangementCollection();
        var mockBehavior = new MockBehavior<IFooActionValueTypeParameterRef<int>>(arrangements);
        var callBehavior = new CallBehavior<IFooActionValueTypeParameterRef<int>>(arrangements, signature, mockBehavior);

        // When
        callBehavior.ReturnsRefParameterSequence("first", 13, 42, 65);

        // Then
        Assert.Single(arrangements);
        var arrangement = arrangements.First() as RefParameterSequenceArrangement<int>;
        Assert.NotNull(arrangement);
    }

    [Fact(DisplayName = "Setup a behavior to throw an exception when a method is called")]
    public void SetupThrowExceptionArrangement()
    {
        // Given
        var type = typeof(IFooActionParameterless);
        var methodName = nameof(IFooActionParameterless.MethodWithoutParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var arrangements = new ArrangementCollection();
        var mockBehavior = new MockBehavior<IFooActionParameterless>(arrangements);
        var callBehavior = new CallBehavior<IFooActionParameterless>(arrangements, signature, mockBehavior);

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
        var type = typeof(IFooActionParameterless);
        var methodName = nameof(IFooActionParameterless.MethodWithoutParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var arrangements = new ArrangementCollection();
        var mockBehavior = new MockBehavior<IFooActionParameterless>(arrangements);
        var callBehavior = new CallBehavior<IFooActionParameterless>(arrangements, signature, mockBehavior);

        // When
        callBehavior.Throws(new Exception());

        // Then
        Assert.Single(arrangements);
        var arrangement = arrangements.First() as ExceptionArrangement;
        Assert.NotNull(arrangement);
    }
}
