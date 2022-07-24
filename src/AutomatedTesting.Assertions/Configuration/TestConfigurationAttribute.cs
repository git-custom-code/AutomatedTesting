namespace CustomCode.AutomatedTesting.Configuration;

using System;
using System.Reflection;
using Xunit.Sdk;

/// <summary>
/// Use this attribute on a "per test level" (i.e. on the method level) to set a custom
/// <see cref="ICallerContext"/> and/or <see cref="IMessageFormatter"/> that should be used by this test.
/// </summary>
/// <example>
/// <![CDATA[
/// [Fact]
/// [TestConfiguration(CallerContext = typeof(MyContext), MessageFormatter = typeof(MyFormattter))]
/// public void TestMethod()
/// {
///   ...
/// }
///]]>
/// </example>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class TestConfigurationAttribute : BeforeAfterTestAttribute
{
    #region Data

    /// <summary>
    /// Gets or sets the type of the custom <see cref="ICallerContext"/> that should be used by the test.
    /// </summary>
    public Type? CallerContext { get; set; }

    /// <summary>
    /// Gets or sets the type of the custom <see cref="IMessageFormatter"/> that should be used by the test.
    /// </summary>
    public Type? MessageFormatter { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// This method is called before the test method is executed.
    /// </summary>
    /// <param name="methodUnderTest"> The method under test. </param>
    public override void Before(MethodInfo methodUnderTest)
    {
        if (CallerContext != null && typeof(ICallerContext).IsAssignableFrom(CallerContext))
        {
            if (Activator.CreateInstance(CallerContext) is ICallerContext context)
            {
                TestConfiguration.SetCallerContextFor(methodUnderTest.Name, context);
            }
        }

        if (MessageFormatter != null && typeof(IMessageFormatter).IsAssignableFrom(MessageFormatter))
        {
            if (Activator.CreateInstance(MessageFormatter) is IMessageFormatter formatter)
            {
                TestConfiguration.SetMessageFormatterFor(methodUnderTest.Name, formatter);
            }
        }
    }

    /// <summary>
    /// This method is called after the test method is executed.
    /// </summary>
    /// <param name="methodUnderTest"> The method under test. </param>
    public override void After(MethodInfo methodUnderTest)
    {
        TestConfiguration.ResetCallerContextFor(methodUnderTest.Name);
        TestConfiguration.ResetMessageFormatterFor(methodUnderTest.Name);
    }

    #endregion
}
