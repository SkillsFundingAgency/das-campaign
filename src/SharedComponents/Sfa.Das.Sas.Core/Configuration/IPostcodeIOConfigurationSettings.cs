using System;

namespace Sfa.Das.Sas.Core.Configuration
{
    public interface IPostcodeIOConfigurationSettings
    {
        Uri PostcodeUrl { get; }
        Uri PostcodeTerminatedUrl { get; }
    }
}