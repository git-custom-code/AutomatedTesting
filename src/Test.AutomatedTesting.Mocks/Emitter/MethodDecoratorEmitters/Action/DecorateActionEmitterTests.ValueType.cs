namespace CustomCode.AutomatedTesting.Mocks.Emitter.Tests
{
    #region Usings

    using Core.Data;
    using System.Linq;
    using TestDomain;
    using Xunit;

    #endregion

    /// <summary>
    /// Automated tests for the <see cref="DecorateActionEmitter"/> type.
    /// </summary>
    public sealed partial class DecorateActionEmitterTests
    {
        [Theory(DisplayName = "MethodDecorateEmitter: Action (value type) with single input parameter")]
        [ClassData(typeof(ValueTypeData))]
        public void ActionValueTypeWithSingleParameterIn<T>(T expectedValue)
            where T : struct
        {
            // Given
            var proxyFactory = Context.ProxyFactory;
            var interceptor = new ActionInterceptor();
            var decoratee = new FooActionValueTypeParameterIn<T>();

            // When
            var foo = proxyFactory.CreateDecorator<IFooActionValueTypeParameterIn<T>>(decoratee, interceptor);
            foo.MethodWithOneParameter(expectedValue);

            // Then
            Assert.NotNull(foo);
            Assert.Equal(1u, decoratee.CallCount);
            Assert.Equal(expectedValue, decoratee.Parameters.SingleOrDefault());
        }
    }
}