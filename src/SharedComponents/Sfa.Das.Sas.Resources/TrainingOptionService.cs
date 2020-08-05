namespace Sfa.Das.Sas.Resources
{
    using System.Collections.Generic;

    public static class TrainingOptionService
    {
        private static readonly Dictionary<string, string> _dictionary;

        static TrainingOptionService()
        {
            _dictionary = new Dictionary<string, string>
            {
                { "dayrelease", TrainingOptions.training_option_dayrelease },
                { "blockrelease", TrainingOptions.training_option_blockrelease },
                { "100percentemployer", TrainingOptions.training_option_100percentemployer }
            };
        }

        public static string GetApprenticeshipLevel(string item)
        {
            if (string.IsNullOrEmpty(item)) return string.Empty;
            return _dictionary.ContainsKey(item) ? _dictionary[item] : string.Empty;
        }
    }
}
