namespace CustomCode.AutomatedTesting.Configuration;

using System;
using Xunit.Sdk;

/// <summary>
/// Use this attribute on a "all test level" (i.e. on the assembly level) to set a custom
/// <see cref="ICallerContext"/> and/or <see cref="IMessageFormatter"/> that should be used
/// by all tests in the given assembly.
/// </summary>
/// <example>
/// <![CDATA[
/// [assembly:GlobalTestConfiguration(
///     CallerContext = typeof(MyContext),
///     MessageFormatter = typeof(MyFormattter))]
/// ]]>
/// </example>
[TestFrameworkDiscoverer("CustomCode.AutomatedTesting.Assertions.Configuration.GlobalTestConfigurationDiscoverer", "CustomCode.AutomatedTesting.Assertions")]
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
public sealed class GlobalTestConfigurationAttribute : Attribute, ITestFrameworkAttribute
{
    #region Data

    /// <summary>
    /// Gets or sets the type of the custom <see cref="ICallerContext"/> that should be used by all tests.
    /// </summary>
    public Type? CallerContext { get; set; }

    /// <summary>
    /// Gets or sets the type of the custom <see cref="IMessageFormatter"/> that should be used by all tests.
    /// </summary>
    public Type? MessageFormatter { get; set; }

    #endregion
}
