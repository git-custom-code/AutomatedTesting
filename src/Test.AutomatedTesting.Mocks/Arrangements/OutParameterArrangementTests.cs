namespace CustomCode.AutomatedTesting.Mocks.Arrangements.Tests;

using ExceptionHandling;
using Interception;
using Interception.Parameters;
using System.Linq;
using TestDomain;
using Xunit;

/// <summary>
/// Automated tests for the <see cref="OutParameterArrangement{T}"/> type.
/// </summary>
public sealed class OutParameterArrangementTests
{
    [Fact(DisplayName = "Apply an arranged out parameter value (value type) to an invocation of a (void) method")]
    public void ApplyValueTypeOutParameterArrangement()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "first", 42);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(42, parameter.Value);
    }

    [Fact(DisplayName = "Apply an arranged out parameter value (value type) to an invocation of a method")]
    public void ApplyValueTypeOutParameterArrangementFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "first", 42);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(42, parameter.Value);
    }

    [Fact(DisplayName = "Apply an arranged out parameter value (reference type) to an invocation of a (void) method")]
    public void ApplyReferenceTypeOutParameterArrangement()
    {
        // Given
        var expectedValue = new object();
        var type = typeof(IFooActionReferenceTypeParameterOut<object>);
        var methodName = nameof(IFooActionReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<object?>(signature, "first", expectedValue);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(object), parameter.Type);
        Assert.Equal(expectedValue, parameter.Value);
    }

    [Fact(DisplayName = "Apply an arranged out parameter value (reference type) to an invocation of a method")]
    public void ApplyReferenceTypeOutParameterArrangementFunc()
    {
        // Given
        var expectedValue = new object();
        var type = typeof(IFooFuncReferenceTypeParameterOut<object>);
        var methodName = nameof(IFooFuncReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<object?>(signature, "first", expectedValue);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(object), parameter.Type);
        Assert.Equal(expectedValue, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that a out parameter arrangement is only applied if the invoked (void) method signature matches")]
    public void EnsureNoArrangentIsAppliedToNonMatchingInvocation()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        type = typeof(IFooActionReferenceTypeParameterOut<object>);
        methodName = nameof(IFooActionReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

        var outParameterFeature = new ParameterOut(valueTypeSignature);
        var invocation = new Invocation(valueTypeSignature, outParameterFeature);
        var arrangment = new OutParameterArrangement<object?>(referenceTypeSignature, "first", new object());

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that a out parameter arrangement is only applied if the invoked (void) method parameter matches")]
    public void EnsureNoArrangentIsAppliedToNonMatchingParameterInvocation()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that a out parameter arrangement is only applied if the invoked method signature matches")]
    public void EnsureNoArrangentIsAppliedToNonMatchingInvocationFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        type = typeof(IFooFuncReferenceTypeParameterOut<object>);
        methodName = nameof(IFooFuncReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

        var outParameterFeature = new ParameterOut(valueTypeSignature);
        var invocation = new Invocation(valueTypeSignature, outParameterFeature);
        var arrangment = new OutParameterArrangement<object?>(referenceTypeSignature, "first", new object());

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that a out parameter arrangement is only applied if the invoked method parameter matches")]
    public void EnsureNoArrangentIsAppliedToNonMatchingParameterInvocationFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns true if the invoked (void) method signature matches")]
    public void CanApplyWithMatchingSignatureReturnsTrue()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "first", 42);

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.True(canApply);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns true if the invoked method signature matches")]
    public void CanApplyWithMatchingSignatureReturnsTrueFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "first", 42);

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.True(canApply);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked (void) method signature does not match")]
    public void CanApplyWithNonMatchingSignatureReturnsFalse()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        type = typeof(IFooActionReferenceTypeParameterOut<object>);
        methodName = nameof(IFooActionReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

        var outParameterFeature = new ParameterOut(valueTypeSignature);
        var invocation = new Invocation(valueTypeSignature, outParameterFeature);
        var arrangment = new OutParameterArrangement<object?>(referenceTypeSignature, "first", new object());

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.False(canApply);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked (void) method parameter does not match")]
    public void CanApplyWithNonMatchingParameterReturnsFalse()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.False(canApply);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked method signature does not match")]
    public void CanApplyWithNonMatchingSignatureReturnsFalseFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        type = typeof(IFooFuncReferenceTypeParameterOut<object>);
        methodName = nameof(IFooFuncReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

        var outParameterFeature = new ParameterOut(valueTypeSignature);
        var invocation = new Invocation(valueTypeSignature, outParameterFeature);
        var arrangment = new OutParameterArrangement<object?>(referenceTypeSignature, "first", new object());

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.False(canApply);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked method parameter does not match")]
    public void CanApplyWithNonMatchingParameterReturnsFalseFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.False(canApply);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Try to apply an arranged out parameter value (value type) to an invocation of a (void) method")]
    public void TryApplyValueTypeOutParameterArrangement()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "first", 42);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.True(wasApplied);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(42, parameter.Value);
    }

    [Fact(DisplayName = "Try to apply an arranged out parameter value (value type) to an invocation of a method")]
    public void TryApplyValueTypeOutParameterArrangementFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "first", 42);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.True(wasApplied);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(42, parameter.Value);
    }

    [Fact(DisplayName = "Try to apply an arranged out parameter value (reference type) to an invocation of a (void) method")]
    public void TryApplyReferenceTypeOutParameterArrangement()
    {
        // Given
        var expectedValue = new object();
        var type = typeof(IFooActionReferenceTypeParameterOut<object>);
        var methodName = nameof(IFooActionReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<object?>(signature, "first", expectedValue);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.True(wasApplied);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(object), parameter.Type);
        Assert.Equal(expectedValue, parameter.Value);
    }

    [Fact(DisplayName = "Try to apply an arranged out parameter value (reference type) to an invocation of a method")]
    public void TryApplyReferenceTypeOutParameterArrangementFunc()
    {
        // Given
        var expectedValue = new object();
        var type = typeof(IFooFuncReferenceTypeParameterOut<object>);
        var methodName = nameof(IFooFuncReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<object?>(signature, "first", expectedValue);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.True(wasApplied);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(object), parameter.Type);
        Assert.Equal(expectedValue, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that TryApplyTo will return false if the invoked (void) method signature does not match")]
    public void EnsureTryApplyIsFalseForNonMatchingInvocation()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.False(wasApplied);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that TryApplyTo will return false if the invoked method signature does not match")]
    public void EnsureTryApplyIsFalseForNonMatchingInvocationFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.False(wasApplied);
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(default(int), parameter.Value);
    }
}
