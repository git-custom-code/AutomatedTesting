namespace CustomCode.AutomatedTesting.Mocks
{
    using System.Linq.Expressions;

    /// <summary>
    /// Fluent API interface that is returned by <see cref="IMockBehavior{TMock}.That(Expression{System.Action{TMock}})"/>
    /// calls and can be used to setup the behavior of mocked method or property calls with <see cref="void"/> return type.
    /// </summary>
    public interface ICallBehavior : ICallBehaviorBase
    { }
}