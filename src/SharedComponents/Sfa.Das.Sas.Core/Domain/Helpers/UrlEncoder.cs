using System.Linq;
using System.Text.RegularExpressions;

namespace Sfa.Das.Sas.Core.Domain.Helpers
{
    public class UrlEncoder : IUrlEncoder
    {
        public string EncodeTextForUri(string textToProcess)
        {
            if (string.IsNullOrEmpty(textToProcess))
            {
                return string.Empty;
            }

            var specialCharactersRemoved = Regex.Replace(textToProcess.ToLower(), @"[',-]", string.Empty);
            var splitBySpacesAndOtherChars = Regex.Split(specialCharactersRemoved, @"[\s(),.:;?!\\/]+");
            var emptyRemoved = splitBySpacesAndOtherChars.Where(s => !string.IsNullOrEmpty(s));
            var rebuildExcludingNoContent = string.Join("-", emptyRemoved);

            var processHyphenPattern = ProcessInitialsHyphenPattern(rebuildExcludingNoContent);

            var ampersandAndPlusReplaced = Regex.Replace(processHyphenPattern, "[&+]", "and");
            return Regex.Escape(ampersandAndPlusReplaced);
        }

        private string ProcessInitialsHyphenPattern(string stringToProcess)
        {
            const string initialsHyphenPattern = @"[a-z0-9]{1}-[a-z0-9]{1}-";
            var match = Regex.Match(stringToProcess, initialsHyphenPattern);
            while (match.Length > 0)
            {
                var originalPattern = match.Value;
                var replacementPattern = originalPattern.Substring(0, 1) + originalPattern.Substring(2, 2);
                stringToProcess = stringToProcess.Replace(originalPattern, replacementPattern);
                match = Regex.Match(stringToProcess, initialsHyphenPattern);
            }

            return stringToProcess;
        }
    }
}