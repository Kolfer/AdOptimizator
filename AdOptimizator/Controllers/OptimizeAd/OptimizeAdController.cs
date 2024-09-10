using AdOptimizer.Models.OptimizeAd;
using AdOptimizer.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AdOptimizer.Controllers.OptimizeAd
{
    [Route("api/")]
    public class OptimizeAdController : ControllerBase
    {
        [HttpPost("optimize-ad")]
        public ActionResult<OptimizeAdResponse> OptimizeAd(OptimizeAdRequest request)
        {
            if (request == null)
            {
                return BadRequest(Errors.EmptyRequest);
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(Errors.EmptyTitle);
            }

            if (!Constants.SocialMedia.Keys.Any(c => c.ToLower().Equals(request.Platform.ToLower())))
            {
                return BadRequest(Errors.UnknownPlatform);
            }

            var characterLimit = Constants.SocialMedia[request.Platform.ToLower()];

            if (request.Title.Length + Constants.DefaultLinkLength > characterLimit)
            {
                return BadRequest(Errors.TitleExceedsCharacterLimit);
            }

            var result = GetFormattedText(request, characterLimit);

            return Content(result);
        }

        private string GetFormattedText(OptimizeAdRequest request, int characterLimit)
        {
            var text = "";
            var description = string.IsNullOrWhiteSpace(request.Description) ? "" : request.Description;

            bool isDescriptionIncluded = request.Title.Length + description.Length < characterLimit && description.Length != 0;

            if (isDescriptionIncluded)
            {
                text = FormatTextWithDescription(request.Title, description, characterLimit, request.Keywords);
            }
            else
            {
                text = FormatTextWithoutDescription(request.Title, characterLimit, request.Keywords);
            }
            return text;
        }

        private string FormatTextWithDescription(string title, string description, int characterLimit, List<string> keywords)
        {
            var text = "";

            var keywordsLength = CountKeywordLength(keywords);
            var titleDecriptionLength = title.Length + description.Length;

            bool AreKeywordsIncluded = (titleDecriptionLength + keywordsLength) < characterLimit && keywordsLength != 0;

            if (AreKeywordsIncluded)
            {
                text =FormatTextWithKeywords(title, description, characterLimit, keywordsLength, keywords);
            }
            else
            {
                text = FormatTextWithoutKeywords(title, description, characterLimit);
            }
            return text;
        }

        private string FormatTextWithoutDescription(string title, int characterLimit, List<string> keywords)
        {
            var text = "";

            var keywordsLength = CountKeywordLength(keywords);
            var titleLinkLength = title.Length + Constants.DefaultLinkLength;

            bool AreKeywordsIncluded = (titleLinkLength + keywordsLength) < characterLimit && keywordsLength != 0;

            if (AreKeywordsIncluded)
            {
                text = FormatTextWithKeywords(title, Constants.LinkText, characterLimit, keywordsLength, keywords);
            }
            else
            {
                text = FormatTextWithoutKeywords(title, Constants.LinkText, characterLimit);
            }
            return text;
        }

        private string FormatTextWithKeywords(string title, string middleText, int characterLimit, int keywordsLength, List<string> keywords)
        {
            string text = "";
            var titleMiddleTextLength = title.Length + middleText.Length;

            if ((titleMiddleTextLength + keywordsLength + Constants.IntroductoryText.Length) < characterLimit)
            {
                text = FormatText(Constants.IntroductoryText + title, middleText, FormatTags(keywords));
            }
            else
            {
                text = FormatText(title, middleText, FormatTags(keywords));
            }
            return text;
        }

        private string FormatTextWithoutKeywords (string title, string middleText, int characterLimit)
        {
            string text = "";
            var titleMiddleTextLength = title.Length + middleText.Length;

            if (titleMiddleTextLength + Constants.IntroductoryText.Length < characterLimit)
            {
                text = FormatText(Constants.IntroductoryText + title, middleText);
            }
            else
            {
                text = FormatText(title, Constants.LinkText);
            }
            return text;
        }

        private static int CountKeywordLength (List<string> keywords)
        {
            var keywordLength = 0;

            if (keywords != null)
            {
                foreach (var key in keywords)
                {
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        keywordLength += key.Length;
                        keywordLength += Constants.KeywordsText.Length;
                    }
                }
            }
            return keywordLength;
        }

        private static string FormatText(string title, string middleText, string? tags = null)
        {
            return $"""
                {title}
                {middleText}{tags}
                """;
        }

        private static string FormatTags(List<string> keywords)
        {
            var tagsText = "";
            foreach (var keyword in keywords)
            {
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    tagsText += Constants.KeywordsText + keyword;
                }
            }
            if (tagsText.Length > 0)
            {
                tagsText = Environment.NewLine + tagsText;
            }
            return tagsText;
        }
    }
}
