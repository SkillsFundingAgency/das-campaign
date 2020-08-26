using System;
using Sfa.Das.Sas.Core.Configuration;

namespace Sfa.Das.Sas.Shared.Components.Configuration
{
    public class PostcodeIOConfiguration : IPostcodeIOConfigurationSettings
    {
        public Uri PostcodeUrl { get; set; }
        public Uri PostcodeTerminatedUrl { get; set; }
    }
}
