# AutomatedTesting
![](https://github.com/git-custom-code/AutomatedTesting/workflows/Build/badge.svg)
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
