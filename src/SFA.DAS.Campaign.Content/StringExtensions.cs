namespace SFA.DAS.Campaign.Content
{
    public static class StringExtensions
    {
        public static string FirstCharacterToLower(this string input)
        {
            return char.ToLowerInvariant(input[0]) + input.Substring(1);
        }
    }
}