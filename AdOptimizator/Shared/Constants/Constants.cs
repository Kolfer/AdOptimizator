﻿namespace AdOptimizer.Shared.Constants
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

        public const string LinkText = "Find out about the position here: [Paste a link to the job here.]";

        public const int DefaultLinkLength = 100;
    }
}
