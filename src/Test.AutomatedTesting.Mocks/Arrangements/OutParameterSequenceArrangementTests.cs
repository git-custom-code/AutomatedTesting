namespace CustomCode.AutomatedTesting.Mocks.Arrangements.Tests;

using ExceptionHandling;
using Interception;
using Interception.Parameters;
using System.Collections.Generic;
using System.Linq;
using TestDomain;
using Xunit;

/// <summary>
/// Automated tests for the <see cref="OutParameterSequenceArrangement{T}"/> type.
/// </summary>
public sealed class OutParameterSequenceArrangementTests
{
    [Fact(DisplayName = "Apply a sequence of out parameter values (value type) to an invocation of a (void) method")]
    public void ApplySequenceOfValueTypeOutParametersArrangement()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "first", new List<int> { 13, 42, 65 });

        // When
        var feature = invocation.GetFeature<IParameterOut>();
        arrangment.ApplyTo(invocation);
        var first = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var second = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var third = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var fourth = feature.OutParameterCollection.First().Value;

        // Then
        Assert.Equal(13, first);
        Assert.Equal(42, second);
        Assert.Equal(65, third);
        Assert.Equal(65, fourth);
    }

    [Fact(DisplayName = "Apply a sequence of out parameter values (value type) to an invocation of a method")]
    public void ApplySequenceOfValueTypeOutParametersArrangementFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "first", new List<int> { 13, 42, 65 });

        // When
        var feature = invocation.GetFeature<IParameterOut>();
        arrangment.ApplyTo(invocation);
        var first = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var second = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var third = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var fourth = feature.OutParameterCollection.First().Value;

        // Then
        Assert.Equal(13, first);
        Assert.Equal(42, second);
        Assert.Equal(65, third);
        Assert.Equal(65, fourth);
    }

    [Fact(DisplayName = "Apply a sequence of out parameter values (reference type) to an invocation of a (void) method")]
    public void ApplySequenceOfReferenceTypeOutParametersArrangement()
    {
        // Given
        var value1 = new object();
        var value2 = new object();
        var value3 = new object();
        var type = typeof(IFooActionReferenceTypeParameterOut<object>);
        var methodName = nameof(IFooActionReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<object?>(
            signature, "first", new List<object?> { value1, value2, value3 });

        // When
        var feature = invocation.GetFeature<IParameterOut>();
        arrangment.ApplyTo(invocation);
        var first = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var second = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var third = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var fourth = feature.OutParameterCollection.First().Value;

        // Then
        Assert.Equal(value1, first);
        Assert.Equal(value2, second);
        Assert.Equal(value3, third);
        Assert.Equal(value3, fourth);
    }

    [Fact(DisplayName = "Apply a sequence of out parameter values (reference type) to an invocation of a method")]
    public void ApplySequenceOfReferenceTypeOutParametersArrangementFunc()
    {
        // Given
        var value1 = new object();
        var value2 = new object();
        var value3 = new object();
        var type = typeof(IFooFuncReferenceTypeParameterOut<object>);
        var methodName = nameof(IFooFuncReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<object?>(
            signature, "first", new List<object?> { value1, value2, value3 });

        // When
        var feature = invocation.GetFeature<IParameterOut>();
        arrangment.ApplyTo(invocation);
        var first = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var second = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var third = feature.OutParameterCollection.First().Value;
        arrangment.ApplyTo(invocation);
        var fourth = feature.OutParameterCollection.First().Value;

        // Then
        Assert.Equal(value1, first);
        Assert.Equal(value2, second);
        Assert.Equal(value3, third);
        Assert.Equal(value3, fourth);
    }

    [Fact(DisplayName = "Ensure that a out parameter sequence arrangement is only applied if the invoked (void) method signature matches")]
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
        var arrangment = new OutParameterSequenceArrangement<object>(
            referenceTypeSignature, "first", new List<object> { new object() });

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that a out parameter sequence arrangement is only applied if the invoked (void) method parameter matches")]
    public void EnsureNoArrangentIsAppliedToNonMatchingParameterInvocation()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 13, 42, 65 });

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that a out parameter sequence arrangement is only applied if the invoked method signature matches")]
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
        var arrangment = new OutParameterSequenceArrangement<object>(
            referenceTypeSignature, "first", new List<object> { new object() });

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterOut>());
        var feature = invocation.GetFeature<IParameterOut>();
        Assert.Single(feature.OutParameterCollection);
        var parameter = feature.OutParameterCollection.Single();
        Assert.Equal(default(int), parameter.Value);
    }

    [Fact(DisplayName = "Ensure that a out parameter sequence arrangement is only applied if the invoked method parameter matches")]
    public void EnsureNoArrangentIsAppliedToNonMatchingParameterInvocationFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 13, 42, 65 });

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
    public void CanApplySequenceWithMatchingSignatureReturnsTrue()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "first", new List<int> { 42 });

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
    public void CanApplySequenceWithMatchingSignatureReturnsTrueFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "first", new List<int> { 42 });

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
    public void CanApplySequenceWithNonMatchingSignatureReturnsFalse()
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
        var arrangment = new OutParameterSequenceArrangement<object>(
            referenceTypeSignature, "first", new List<object> { new object() });

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
    public void CanApplySequenceWithNonMatchingParameterReturnsFalse()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 42 });

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
    public void CanApplySequenceWithNonMatchingSignatureReturnsFalseFunc()
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
        var arrangment = new OutParameterSequenceArrangement<object>(
            referenceTypeSignature, "first", new List<object> { new object() });

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
    public void CanApplySequenceWithNonMatchingParameterReturnsFalseFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 42 });

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

    [Fact(DisplayName = "Try to apply a sequence of out parameter values (value type) to an invocation of a (void) method")]
    public void TryApplySequenceOfValueTypeOutParametersArrangement()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "first", new List<int> { 13, 42, 65 });

        // When
        var feature = invocation.GetFeature<IParameterOut>();
        var wasFirstApplied = arrangment.TryApplyTo(invocation);
        var first = feature.OutParameterCollection.First().Value;
        var wasSecondApplied = arrangment.TryApplyTo(invocation);
        var second = feature.OutParameterCollection.First().Value;
        var wasThirdApplied = arrangment.TryApplyTo(invocation);
        var third = feature.OutParameterCollection.First().Value;
        var wasFourthApplied = arrangment.TryApplyTo(invocation);
        var fourth = feature.OutParameterCollection.First().Value;

        // Then
        Assert.True(wasFirstApplied);
        Assert.Equal(13, first);
        Assert.True(wasSecondApplied);
        Assert.Equal(42, second);
        Assert.True(wasThirdApplied);
        Assert.Equal(65, third);
        Assert.True(wasFourthApplied);
        Assert.Equal(65, fourth);
    }

    [Fact(DisplayName = "Try to apply a sequence of out parameter values (value type) to an invocation of a method")]
    public void TryApplySequenceOfValueTypeOutParametersArrangementFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "first", new List<int> { 13, 42, 65 });

        // When
        var feature = invocation.GetFeature<IParameterOut>();
        var wasFirstApplied = arrangment.TryApplyTo(invocation);
        var first = feature.OutParameterCollection.First().Value;
        var wasSecondApplied = arrangment.TryApplyTo(invocation);
        var second = feature.OutParameterCollection.First().Value;
        var wasThirdApplied = arrangment.TryApplyTo(invocation);
        var third = feature.OutParameterCollection.First().Value;
        var wasFourthApplied = arrangment.TryApplyTo(invocation);
        var fourth = feature.OutParameterCollection.First().Value;

        // Then
        Assert.True(wasFirstApplied);
        Assert.Equal(13, first);
        Assert.True(wasSecondApplied);
        Assert.Equal(42, second);
        Assert.True(wasThirdApplied);
        Assert.Equal(65, third);
        Assert.True(wasFourthApplied);
        Assert.Equal(65, fourth);
    }

    [Fact(DisplayName = "Try to apply a sequence of out parameter values (reference type) to an invocation of a (void) method")]
    public void TryApplySequenceOfReferenceTypeOutParametersArrangement()
    {
        // Given
        var value1 = new object();
        var value2 = new object();
        var value3 = new object();
        var type = typeof(IFooActionReferenceTypeParameterOut<object>);
        var methodName = nameof(IFooActionReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<object?>(
           signature, "first", new List<object?> { value1, value2, value3 });

        // When
        var feature = invocation.GetFeature<IParameterOut>();
        var wasFirstApplied = arrangment.TryApplyTo(invocation);
        var first = feature.OutParameterCollection.First().Value;
        var wasSecondApplied = arrangment.TryApplyTo(invocation);
        var second = feature.OutParameterCollection.First().Value;
        var wasThirdApplied = arrangment.TryApplyTo(invocation);
        var third = feature.OutParameterCollection.First().Value;
        var wasFourthApplied = arrangment.TryApplyTo(invocation);
        var fourth = feature.OutParameterCollection.First().Value;

        // Then
        Assert.True(wasFirstApplied);
        Assert.Equal(value1, first);
        Assert.True(wasSecondApplied);
        Assert.Equal(value2, second);
        Assert.True(wasThirdApplied);
        Assert.Equal(value3, third);
        Assert.True(wasFourthApplied);
        Assert.Equal(value3, fourth);
    }

    [Fact(DisplayName = "Try to apply a sequence of out parameter values (reference type) to an invocation of a method")]
    public void TryApplySequenceOfReferenceTypeOutParametersArrangementFunc()
    {
        // Given
        var value1 = new object();
        var value2 = new object();
        var value3 = new object();
        var type = typeof(IFooFuncReferenceTypeParameterOut<object>);
        var methodName = nameof(IFooFuncReferenceTypeParameterOut<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<object?>(
            signature, "first", new List<object?> { value1, value2, value3 });

        // When
        var feature = invocation.GetFeature<IParameterOut>();
        var wasFirstApplied = arrangment.TryApplyTo(invocation);
        var first = feature.OutParameterCollection.First().Value;
        var wasSecondApplied = arrangment.TryApplyTo(invocation);
        var second = feature.OutParameterCollection.First().Value;
        var wasThirdApplied = arrangment.TryApplyTo(invocation);
        var third = feature.OutParameterCollection.First().Value;
        var wasFourthApplied = arrangment.TryApplyTo(invocation);
        var fourth = feature.OutParameterCollection.First().Value;

        // Then
        Assert.True(wasFirstApplied);
        Assert.Equal(value1, first);
        Assert.True(wasSecondApplied);
        Assert.Equal(value2, second);
        Assert.True(wasThirdApplied);
        Assert.Equal(value3, third);
        Assert.True(wasFourthApplied);
        Assert.Equal(value3, fourth);
    }

    [Fact(DisplayName = "Ensure that TryApplyTo will return false if the invoked (void) method signature does not match")]
    public void EnsureTryApplySequenceIsFalseForNonMatchingInvocation()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterOut<int>);
        var methodName = nameof(IFooActionValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 13, 42, 65 });

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
    public void EnsureTryApplySequenceIsFalseForNonMatchingInvocationFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterOut<int>);
        var methodName = nameof(IFooFuncValueTypeParameterOut<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var outParameterFeature = new ParameterOut(signature);
        var invocation = new Invocation(signature, outParameterFeature);
        var arrangment = new OutParameterSequenceArrangement<int>(signature, "WrongParameterName", new List<int> { 13, 42, 65 });

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
