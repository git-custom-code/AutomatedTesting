namespace CustomCode.AutomatedTesting.Configuration;

using System;
using Xunit.Abstractions;
using Xunit.Sdk;

/// <summary>
/// Discover the <see cref="GlobalTestConfigurationAttribute"/> and initialize the global
/// <see cref="TestConfiguration"/>.
/// </summary>
public sealed class GlobalTestConfigurationDiscoverer : ITestFrameworkTypeDiscoverer
{
    #region Logic

    /// <summary>
    /// Gets the type that implements <see cref="ITestFramework"/> to be used to discover and run tests.
    /// </summary>
    /// <param name="attribute"> The <see cref="GlobalTestConfigurationAttribute"/>. </param>
    /// <returns> Always returns the <see cref="XunitTestFramework"/> type. </returns>
    public Type GetTestFrameworkType(IAttributeInfo attribute)
    {
        ICallerContext? callerContext = null;
        var callerContextType = attribute.GetNamedArgument<Type>(nameof(GlobalTestConfigurationAttribute.CallerContext));
        if (callerContextType != null && typeof(ICallerContext).IsAssignableFrom(callerContextType))
        {
            callerContext = Activator.CreateInstance(callerContextType) as ICallerContext;
        }

        IMessageFormatter? messageFormatter = null;
        var messageFormatterType = attribute.GetNamedArgument<Type>(nameof(GlobalTestConfigurationAttribute.MessageFormatter));
        if (messageFormatterType != null && typeof(IMessageFormatter).IsAssignableFrom(messageFormatterType))
        {
            messageFormatter = Activator.CreateInstance(messageFormatterType) as IMessageFormatter;
        }

        TestConfiguration.Initialize(callerContext, messageFormatter);
        return typeof(XunitTestFramework);
    }

    #endregion
}
