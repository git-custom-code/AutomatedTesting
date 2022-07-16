# AutomatedTesting
![Build](https://github.com/git-custom-code/AutomatedTesting/workflows/Build/badge.svg)
![Unit Tests](https://github.com/git-custom-code/AutomatedTesting/workflows/Unit%20Tests/badge.svg)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Mocks (Unit Test)
If you want to test a single class (e.g. "Foo") in isolation and have all dependencies (e.g. "IFooDependency") replaced by mocks, you can do so by simply calling "Mock.CreateMocked<ClassToTest>()". The behavior of the mocked dependecies can be changed by calling the fluent "ArrangeFor<MockedDependency>()..." api.
  
```csharp
public sealed class FooTests
{
    [Fact]
    void UnitTest()
    {
        // Given
        var mocked = Mock.CreateMocked<Foo>();
        mocked.ArrangeFor<IFooDependency>().That(m => m.ReturnSomething()).Returns(42);

        // When
        var result = mocked.Instance.MethodToBeTested();

        // Then
        Assert.Equal(42, result);
    }
}
```

## Mocks (Integration Test)
If you want to test the interaction of multiple classes (testing as much of the productive source code as possible) you can do so by creating a partial mock (via calling "Mock.CreatePartialMock<ClassToTest>"). Any dependencies are resolved by calls to the specified service container and therefore will invoke the "original" source code. Only if you setup any arrangements (by calling the fluent api) those arrangements will be inovked instead.

```csharp
public sealed class FooTests
{
    [Fact]
    void IntegrationTest()
    {
        // Given
        var container = new ServiceContainer();
        ... // setup container
        var mocked = Mock.CreatePartialMocked<Foo>(container);
        mocked.ArrangeFor<IFooDependency>().That(m => m.ReturnSomething()).Returns(42);

        // When
        var result = mocked.Instance.MethodToBeTested();

        // Then
        Assert.Equal(42, result);
    }
}
```
