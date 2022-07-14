namespace CustomCode.AutomatedTesting.Mocks;

using Arrangements;
using ExceptionHandling;
using Emitter;
using Interception.Internal;
using LightInject;
using System;
using System.Linq.Expressions;
using System.Reflection;

/// <summary>
/// Default implementation of the <see cref="IMockBehavior{TMock}"/> interface.
/// </summary>
/// <typeparam name="TMock"> The type of the interface that is mocked. </typeparam>
public sealed class MockBehavior<TMock> : IMockBehavior<TMock>
    where TMock : class
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="MockBehavior{TMock}"/> type.
    /// </summary>
    /// <param name="arrangements">
    /// The collection of arrangements that have been made for mocks of type <typeparamref name="TMock"/>.
    /// </param>
    public MockBehavior(IArrangementCollection arrangements)
    {
        Arrangements = arrangements ?? throw new ArgumentNullException(nameof(arrangements));
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the collection of arrangements that have been made for mocks of type <typeparamref name="TMock"/>.
    /// </summary>
    private IArrangementCollection Arrangements { get; }

    /// <summary>
    /// Gets a lazy loaded <see cref="IDynamicProxyFactory"/> instance.
    /// </summary>
    private static Lazy<IDynamicProxyFactory> ProxyFactory { get; } = new Lazy<IDynamicProxyFactory>(CreateProxyFactory, true);

    #endregion

    #region Logic

    /// <inheritdoc cref="IMockBehavior{TMock}" />
    public ICallBehavior<TMock> That(Expression<Action<TMock>> mockedCall)
    {
        Ensures.NotNull(mockedCall, nameof(mockedCall));

        if (mockedCall.Body is MethodCallExpression methodCall)
        {
            return new CallBehavior<TMock>(Arrangements, methodCall.Method, this);
        }

        throw new NotImplementedException();
    }

    /// <inheritdoc cref="IMockBehavior{TMock}" />
    public ICallBehavior<TMock, TResult> That<TResult>(Expression<Func<TMock, TResult>> mockedCall)
    {
        Ensures.NotNull(mockedCall, nameof(mockedCall));

        if (mockedCall.Body is MethodCallExpression methodCall)
        {
            return new CallBehavior<TMock, TResult>(Arrangements, methodCall.Method, this);
        }
        else if (mockedCall.Body is MemberExpression expression)
        {
            if (expression.Member is PropertyInfo signature && signature.CanRead)
            {
                var getter = signature.GetGetMethod() ?? throw new Exception($"Property {signature.Name} has no getter");
                return new CallBehavior<TMock, TResult>(Arrangements, getter, this);
            }
        }

        throw new NotImplementedException();
    }

    /// <inheritdoc cref="IMockBehavior{TMock}" />
    public ICallBehavior<TMock> ThatAssigning(Action<TMock> mockedSetterCall)
    {
        Ensures.NotNull(mockedSetterCall, nameof(mockedSetterCall));

        var interceptor = new PropertySetterInterceptor();
        try
        {
            var proxy = ProxyFactory.Value.CreateForInterface<TMock>(interceptor);
            mockedSetterCall(proxy);
        }
        catch (Exception e)
        {
            throw new Exception("Action is no valid property setter", e);
        }

        var setter = interceptor.DiscoveredSetter ?? throw new Exception($"Property has no setter");
        return new CallBehavior<TMock>(Arrangements, setter, this);
    }

    /// <summary>
    /// Create a new <see cref="IDynamicProxyFactory"/> instance for the <see cref="ProxyFactory"/> property.
    /// </summary>
    /// <returns> The newly created <see cref="IDynamicProxyFactory"/> instance. </returns>
    private static IDynamicProxyFactory CreateProxyFactory()
    {
        using var iocContainer = new ServiceContainer();
        iocContainer.RegisterAssembly(typeof(MockBehavior).Assembly);
        var factory = iocContainer.GetInstance<IDynamicProxyFactory>();
        return factory;
    }

    #endregion
}
