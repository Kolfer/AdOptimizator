namespace AdOptimizer.Shared.Constants
{
    public static class Errors
    {
        public const string EmptyRequest = "Request is empty.";

        public const string EmptyTitle = "Title cannot be empty.";

        public const string EmptyPlatform = "Platform cannot be empty.";

        public const string UnknownPlatform = "Platform is unknown.";

        public const string TitleExceedsCharacterLimit = "Title and link won't fit in post. Try decreasing size of the title.";

        public const string NegativeCharacterLimit = "Character limit cannot be a negative number.";

        public const string NoPlatformFitsCharacterLimit = "There is no social media that support provided character limit: {0}.";

        public const string EmptyPlatformList = "The list of platforms is emtpy.";
    }
}
