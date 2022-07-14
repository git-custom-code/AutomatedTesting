namespace CustomCode.AutomatedTesting.Mocks.Fluent.Tests;

using Arrangements;
using TestDomain;
using Xunit;

/// <summary>
/// Automated tests for the <see cref="MockBehavior"/> type.
/// </summary>
public sealed class MockBehaviorTests
{
    #region ValueTypeAction

    [Fact(DisplayName = "Setup the behavior of a mocked method with void return type and without parameters")]
    public void SetupBehaviorOfValueTypeActionWithoutParameter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooActionParameterless>(arrangements);

        // When
        var callArrangements = mockArrangements.That(b => b.MethodWithoutParameter());

        // Then
        Assert.NotNull(callArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooActionParameterless>>(callArrangements);
    }

    [Fact(DisplayName = "Setup the call behavior of a mocked method with void return type and one parameter")]
    public void SetupBehaviorOfValueTypeActionWithOneParameter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooActionValueTypeParameterIn<int>>(arrangements);

        // When
        var callArrangements = mockArrangements.That(f => f.MethodWithOneParameter(0));

        // Then
        Assert.NotNull(callArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooActionValueTypeParameterIn<int>>>(callArrangements);
    }

    #endregion

    #region ReferenceTypeAction

    [Fact(DisplayName = "Setup the call behavior of a mocked method with void return type and one parameter")]
    public void SetupBehaviorOfReferenceTypeActionWithOneParameter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooActionValueTypeParameterIn<int>>(arrangements);

        // When
        var callArrangements = mockArrangements.That(f => f.MethodWithOneParameter(0));

        // Then
        Assert.NotNull(callArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooActionValueTypeParameterIn<int>>>(callArrangements);
    }

    #endregion

    #region ValueTypeFunc

    [Fact(DisplayName = "Setup the behavior of a mocked method with value type result value and without parameters")]
    public void SetupBehaviorOfValueTypeFuncWithoutParameter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooFuncValueTypeParameterless<int>>(arrangements);

        // When
        var callArrangements = mockArrangements.That(b => b.MethodWithoutParameter());

        // Then
        Assert.NotNull(callArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooFuncValueTypeParameterless<int>, int>>(callArrangements);
    }

    [Fact(DisplayName = "Setup the call behavior of a mocked method with value type result value and one parameter")]
    public void SetupBehaviorOfValueTypeFuncWithOneParameter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooFuncValueTypeParameterIn<int>>(arrangements);

        // When
        var callArrangements = mockArrangements.That(f => f.MethodWithOneParameter(0));

        // Then
        Assert.NotNull(callArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooFuncValueTypeParameterIn<int>, int>>(callArrangements);
    }

    #endregion

    #region ReferenceTypeFunc

    [Fact(DisplayName = "Setup the behavior of a mocked method with reference type result value and without parameters")]
    public void SetupBehaviorOfReferenceTypeFuncWithoutParameter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooFuncReferenceTypeParameterless<object>>(arrangements);

        // When
        var callArrangements = mockArrangements.That(b => b.MethodWithoutParameter());

        // Then
        Assert.NotNull(callArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooFuncReferenceTypeParameterless<object>, object?>>(callArrangements);
    }

    [Fact(DisplayName = "Setup the call behavior of a mocked method with reference type result value and one parameter")]
    public void SetupBehaviorOfReferenceTypeFuncWithOneParameter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooFuncReferenceTypeParameterIn<object>>(arrangements);

        // When
        var callArrangements = mockArrangements.That(f => f.MethodWithOneParameter(0));

        // Then
        Assert.NotNull(callArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooFuncReferenceTypeParameterIn<object>, object?>>(callArrangements);
    }

    #endregion

    #region ValueTypeProperties

    [Fact(DisplayName = "Setup the call behavior of a mocked value type property getter")]
    public void SetupBehaviorOfValueTypePropertyGetter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooValueTypeGetter<int>>(arrangements);

        // When
        var callArrangements = mockArrangements.That(f => f.Getter);

        // Then
        Assert.NotNull(callArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooValueTypeGetter<int>, int>>(callArrangements);
    }

    [Fact(DisplayName = "Setup the call behavior of a mocked value type property setter")]
    public void SetupBehaviorOfValueTypePropertySetter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooValueTypeSetter<int>>(arrangements);

        // When
        var callArrangements = mockArrangements.ThatAssigning(b => b.Setter = 0);

        // Then
        Assert.NotNull(callArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooValueTypeSetter<int>>>(callArrangements);
    }

    [Fact(DisplayName = "Setup the call behavior of a mocked value type property")]
    public void SetupBehaviorOfValueTypePropertyGetterSetter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooValueTypeProperty<int>>(arrangements);

        // When
        var getterArrangements = mockArrangements.That(b => b.GetterSetter);
        var setterArrangements = mockArrangements.ThatAssigning(b => b.GetterSetter = 0);

        // Then
        Assert.NotNull(getterArrangements);
        Assert.NotNull(setterArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooValueTypeProperty<int>, int>>(getterArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooValueTypeProperty<int>>>(setterArrangements);
    }

    #endregion

    #region ReferenceTypeProperties

    [Fact(DisplayName = "Setup the call behavior of a mocked reference type property getter")]
    public void SetupBehaviorOfReferenceTypePropertyGetter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooReferenceTypeGetter<object>>(arrangements);

        // When
        var callArrangements = mockArrangements.That(f => f.Getter);

        // Then
        Assert.NotNull(callArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooReferenceTypeGetter<object>, object?>>(callArrangements);
    }

    [Fact(DisplayName = "Setup the call behavior of a mocked reference type property setter")]
    public void SetupBehaviorOfReferenceTypePropertySetter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooReferenceTypeSetter<object>>(arrangements);

        // When
        var callArrangements = mockArrangements.ThatAssigning(b => b.Setter = 0);

        // Then
        Assert.NotNull(callArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooReferenceTypeSetter<object>>>(callArrangements);
    }

    [Fact(DisplayName = "Setup the call behavior of a mocked reference type property")]
    public void SetupBehaviorOfReferenceTypePropertyGetterSetter()
    {
        // Given
        var arrangements = new ArrangementCollection();
        var mockArrangements = new MockBehavior<IFooReferenceTypeProperty<object>>(arrangements);

        // When
        var getterArrangements = mockArrangements.That(b => b.GetterSetter);
        var setterArrangements = mockArrangements.ThatAssigning(b => b.GetterSetter = 0);

        // Then
        Assert.NotNull(getterArrangements);
        Assert.NotNull(setterArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooReferenceTypeProperty<object>, object?>>(getterArrangements);
        Assert.IsAssignableFrom<ICallBehavior<IFooReferenceTypeProperty<object>>>(setterArrangements);
    }

    #endregion
}
