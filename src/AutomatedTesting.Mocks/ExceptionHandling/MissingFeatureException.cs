namespace CustomCode.AutomatedTesting.Mocks.ExceptionHandling;

using Interception;
using System;

/// <summary>
/// Exception that is thrown whenever an invocation feature was requested via
/// <see cref="IInvocation.GetFeature{T}"/>/ that is not present.
/// </summary>
public sealed class MissingFeatureException : Exception
{
    #region Dependencies

    /// <summary>
    /// Creates a new instance of the <see cref="MissingFeatureException"/> type.
    /// </summary>
    /// <param name="invocation"> The <see cref="IInvocation"/> that is missing a feature. </param>
    /// <param name="featureType"> The <see cref="Type"/> of the feature that is missing. </param>
    public MissingFeatureException(IInvocation invocation, Type featureType)
        : base($"Invocation of method '{invocation.Signature.Name}' has no feature '{featureType.Name}'")
    {
        Invocation = invocation;
        FeatureType = featureType;
    }

    #endregion

    #region Data

    /// <summary>
    /// Gets the <see cref="IInvocation"/> that is missing a feature.
    /// </summary>
    public IInvocation Invocation { get; }


    /// <summary>
    /// Gets the <see cref="Type"/> of the feature that is missing.
    /// </summary>
    public Type FeatureType { get; }

    #endregion
}
