namespace CustomCode.AutomatedTesting.Mocks.Arrangements.Tests;

using ExceptionHandling;
using Interception;
using Interception.Parameters;
using System.Linq;
using TestDomain;
using Xunit;

/// <summary>
/// Automated tests for the <see cref="RefParameterArrangement{T}"/> type.
/// </summary>
public sealed class RefParameterArrangementTests
{
    [Fact(DisplayName = "Apply an arranged ref parameter value (value type) to an invocation of a (void) method")]
    public void ApplyValueTypeRefParameterArrangement()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterRef<int>);
        var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "first", 42);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(42, parameter.Value);
    }

    [Fact(DisplayName = "Apply an arranged ref parameter value (value type) to an invocation of a method")]
    public void ApplyValueTypeRefParameterArrangementFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterRef<int>);
        var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "first", 42);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(42, parameter.Value);
    }

    [Fact(DisplayName = "Apply an arranged ref parameter value (reference type) to an invocation of a (void) method")]
    public void ApplyReferenceTypeRefParameterArrangement()
    {
        // Given
        var expectedValue = new object();
        var type = typeof(IFooActionReferenceTypeParameterRef<object>);
        var methodName = nameof(IFooActionReferenceTypeParameterRef<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { null });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<object?>(signature, "first", expectedValue);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(object), parameter.Type);
        Assert.Equal(expectedValue, parameter.Value);
    }

    [Fact(DisplayName = "Apply an arranged ref parameter value (reference type) to an invocation of a method")]
    public void ApplyReferenceTypeRefParameterArrangementFunc()
    {
        // Given
        var expectedValue = new object();
        var type = typeof(IFooFuncReferenceTypeParameterRef<object>);
        var methodName = nameof(IFooFuncReferenceTypeParameterRef<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { null });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<object?>(signature, "first", expectedValue);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(object), parameter.Type);
        Assert.Equal(expectedValue, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that a ref parameter arrangement is only applied if the invoked (void) method signature matches")]
    public void EnsureNoArrangentIsAppliedToNonMatchingInvocation()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterRef<int>);
        var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
        var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        type = typeof(IFooActionReferenceTypeParameterRef<object>);
        methodName = nameof(IFooActionReferenceTypeParameterRef<object>.MethodWithOneParameter);
        var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

        var refParameterFeature = new ParameterRef(valueTypeSignature, new object?[] { 13 });
        var invocation = new Invocation(valueTypeSignature, refParameterFeature);
        var arrangment = new RefParameterArrangement<object?>(referenceTypeSignature, "first", new object());

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal(13, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that a ref parameter arrangement is only applied if the invoked (void) method parameter matches")]
    public void EnsureNoArrangentIsAppliedToNonMatchingParameterInvocation()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterRef<int>);
        var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal(13, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that a ref parameter arrangement is only applied if the invoked method signature matches")]
    public void EnsureNoArrangentIsAppliedToNonMatchingInvocationFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterRef<int>);
        var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
        var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        type = typeof(IFooFuncReferenceTypeParameterRef<object>);
        methodName = nameof(IFooFuncReferenceTypeParameterRef<object>.MethodWithOneParameter);
        var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

        var refParameterFeature = new ParameterRef(valueTypeSignature, new object?[] { 13 });
        var invocation = new Invocation(valueTypeSignature, refParameterFeature);
        var arrangment = new RefParameterArrangement<object?>(referenceTypeSignature, "first", new object());

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal(13, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that a ref parameter arrangement is only applied if the invoked method parameter matches")]
    public void EnsureNoArrangentIsAppliedToNonMatchingParameterInvocationFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterRef<int>);
        var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        arrangment.ApplyTo(invocation);

        // Then
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal(13, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns true if the invoked (void) method signature matches")]
    public void CanApplyWithMatchingSignatureReturnsTrue()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterRef<int>);
        var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "first", 42);

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.True(canApply);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal(13, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns true if the invoked method signature matches")]
    public void CanApplyWithMatchingSignatureReturnsTrueFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterRef<int>);
        var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "first", 42);

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.True(canApply);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal(13, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked (void) method signature does not match")]
    public void CanApplyWithNonMatchingSignatureReturnsFalse()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterRef<int>);
        var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
        var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        type = typeof(IFooActionReferenceTypeParameterRef<object>);
        methodName = nameof(IFooActionReferenceTypeParameterRef<object>.MethodWithOneParameter);
        var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

        var refParameterFeature = new ParameterRef(valueTypeSignature, new object?[] { 13 });
        var invocation = new Invocation(valueTypeSignature, refParameterFeature);
        var arrangment = new RefParameterArrangement<object?>(referenceTypeSignature, "first", new object());

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.False(canApply);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal(13, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked (void) method parameter does not match")]
    public void CanApplyWithNonMatchingParameterReturnsFalse()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterRef<int>);
        var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.False(canApply);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal(13, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked method signature does not match")]
    public void CanApplyWithNonMatchingSignatureReturnsFalseFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterRef<int>);
        var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
        var valueTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        type = typeof(IFooFuncReferenceTypeParameterRef<object>);
        methodName = nameof(IFooFuncReferenceTypeParameterRef<object>.MethodWithOneParameter);
        var referenceTypeSignature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);

        var refParameterFeature = new ParameterRef(valueTypeSignature, new object?[] { 13 });
        var invocation = new Invocation(valueTypeSignature, refParameterFeature);
        var arrangment = new RefParameterArrangement<object?>(referenceTypeSignature, "first", new object());

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.False(canApply);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal(13, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that CanApplyTo returns false if the invoked method parameter does not match")]
    public void CanApplyWithNonMatchingParameterReturnsFalseFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterRef<int>);
        var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        var canApply = arrangment.CanApplyTo(invocation);

        // Then
        Assert.False(canApply);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal(13, parameter.Value);
    }

    [Fact(DisplayName = "Try to apply an arranged ref parameter value (value type) to an invocation of a (void) method")]
    public void TryApplyValueTypeRefParameterArrangement()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterRef<int>);
        var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "first", 42);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.True(wasApplied);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(42, parameter.Value);
    }

    [Fact(DisplayName = "Try to apply an arranged ref parameter value (value type) to an invocation of a method")]
    public void TryApplyValueTypeRefParameterArrangementFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterRef<int>);
        var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "first", 42);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.True(wasApplied);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(42, parameter.Value);
    }

    [Fact(DisplayName = "Try to apply an arranged ref parameter value (reference type) to an invocation of a (void) method")]
    public void TryApplyReferenceTypeRefParameterArrangement()
    {
        // Given
        var expectedValue = new object();
        var type = typeof(IFooActionReferenceTypeParameterRef<object>);
        var methodName = nameof(IFooActionReferenceTypeParameterRef<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { null });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<object?>(signature, "first", expectedValue);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.True(wasApplied);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(object), parameter.Type);
        Assert.Equal(expectedValue, parameter.Value);
    }

    [Fact(DisplayName = "Try to apply an arranged ref parameter value (reference type) to an invocation of a method")]
    public void TryApplyReferenceTypeRefParameterArrangementFunc()
    {
        // Given
        var expectedValue = new object();
        var type = typeof(IFooFuncReferenceTypeParameterRef<object>);
        var methodName = nameof(IFooFuncReferenceTypeParameterRef<object>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { null });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<object?>(signature, "first", expectedValue);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.True(wasApplied);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(object), parameter.Type);
        Assert.Equal(expectedValue, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that TryApplyTo will return false if the invoked (void) method signature does not match")]
    public void EnsureTryApplyIsFalseForNonMatchingInvocation()
    {
        // Given
        var type = typeof(IFooActionValueTypeParameterRef<int>);
        var methodName = nameof(IFooActionValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.False(wasApplied);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(13, parameter.Value);
    }

    [Fact(DisplayName = "Ensure that TryApplyTo will return false if the invoked method signature does not match")]
    public void EnsureTryApplyIsFalseForNonMatchingInvocationFunc()
    {
        // Given
        var type = typeof(IFooFuncValueTypeParameterRef<int>);
        var methodName = nameof(IFooFuncValueTypeParameterRef<int>.MethodWithOneParameter);
        var signature = type.GetMethod(methodName) ?? throw new MethodInfoException(type, methodName);
        var refParameterFeature = new ParameterRef(signature, new object?[] { 13 });
        var invocation = new Invocation(signature, refParameterFeature);
        var arrangment = new RefParameterArrangement<int>(signature, "WrongParameterName", 42);

        // When
        var wasApplied = arrangment.TryApplyTo(invocation);

        // Then
        Assert.False(wasApplied);
        Assert.True(invocation.HasFeature<IParameterRef>());
        var feature = invocation.GetFeature<IParameterRef>();
        Assert.Single(feature.RefParameterCollection);
        var parameter = feature.RefParameterCollection.Single();
        Assert.Equal("first", parameter.Name);
        Assert.Equal(typeof(int), parameter.Type);
        Assert.Equal(13, parameter.Value);
    }
}
