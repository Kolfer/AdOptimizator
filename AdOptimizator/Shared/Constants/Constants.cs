namespace AdOptimizator.Shared.Constants
{
    public static class Constants
    {
        public static readonly Dictionary<string, int> SocialMedia = new()
        {
            { "twitter", 280 },
            { "linkedin", 1300 }
        };

        public const string IntroductoryText = "We're hiring: ";

        public const string KeywordsText = " #";

        public const string LinkText = "[Paste a link to the job position here.]";

        public const string LongTitleText = "Title with link does not fit in post character limit.";

        public const int DefaultLinkLenght = 80;
    }
}
