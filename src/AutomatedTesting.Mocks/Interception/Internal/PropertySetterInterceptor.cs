namespace CustomCode.AutomatedTesting.Mocks.Interception.Internal;

using ExceptionHandling;
using Fluent;
using Properties;
using System;
using System.Reflection;

/// <summary>
/// Implementation of an <see cref="IInterceptor"/> that is used to discover the called
/// property setter defined within an <see cref="Action{T}"/> delegate.
/// </summary>
/// <remarks>
/// This interceptor is used in <see cref="IMockBehavior{TMock}.ThatAssigning(Action{TMock})"/>.
/// </remarks>
public sealed class PropertySetterInterceptor : IInterceptor
{
    #region Data

    /// <summary>
    /// Gets the signature of the discovered property setter.
    /// </summary>
    public MethodInfo? DiscoveredSetter { get; private set; }

    #endregion

    #region Logic

    /// <inheritdoc cref="IInterceptor" />
    public bool Intercept(IInvocation invocation)
    {
        Ensures.NotNull(invocation, nameof(invocation));

        if (invocation.TryGetFeature<IPropertySetterValue>(out var setter))
        {
            if (DiscoveredSetter == null)
            {
                DiscoveredSetter = setter.Signature.GetSetMethod();
                return true;
            }
            else
            {
                throw new Exception("Discovered more than one property setter call");
            }
        }

        return false;
    }

    #endregion
}
