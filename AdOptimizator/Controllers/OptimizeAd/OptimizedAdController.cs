using AdOptimizator.Models.OptimizeAd;
using AdOptimizator.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AdOptimizator.Controllers.OptimizeAd
{
    [Route("api/")]
    public class OptimizedAdController : ControllerBase
    {
        [HttpPost("optimize-ad")]
        public ActionResult<OptimizeAdResponse> OptimizeAd(OptimizeAdRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request is empty.");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("Title cannot be empty.");
            }

            if (!Constants.SocialMedia.Keys.Any(c => c.ToLower().Equals(request.Platform.ToLower())))
            {
                return BadRequest("Platform is unknown.");
            }

            var result = GetFormattedText(request);

            return Content(result);
        }

        private string GetFormattedText(OptimizeAdRequest request)
        {
            var text = """ """;
            var characterLimit = Constants.SocialMedia[request.Platform.ToLower()];
            var description = string.IsNullOrWhiteSpace(request.Description) ? "" : request.Description;

            var keywordsLength = CountKeywordLength(request.Keywords);

            if (request.Title.Length + description.Length < characterLimit && description.Length != 0)
            {
                var titleDecriptionLenght = request.Title.Length + description.Length;

                if ((titleDecriptionLenght + keywordsLength) < characterLimit && keywordsLength != 0)
                {
                    if ((titleDecriptionLenght + keywordsLength + Constants.IntroductoryText.Length) < characterLimit)
                    {
                        text = FormatText(Constants.IntroductoryText + request.Title, description, FormatTags(request.Keywords));
                    }
                    else
                    {
                        text = FormatText(request.Title, description, FormatTags(request.Keywords));
                    }
                }
                else
                {
                    if (titleDecriptionLenght + Constants.IntroductoryText.Length < characterLimit)
                    {
                        text = FormatText(Constants.IntroductoryText + request.Title, description);
                    }
                    else
                    {
                        text = FormatText(request.Title, description);
                    }
                }
            }
            else
            {
                var titleLinkLenght = request.Title.Length + Constants.DefaultLinkLenght;

                if ((titleLinkLenght + keywordsLength) < characterLimit && keywordsLength != 0)
                {
                    if ((titleLinkLenght + keywordsLength + Constants.IntroductoryText.Length) < characterLimit)
                    {
                        text = FormatText(Constants.IntroductoryText + request.Title, Constants.LinkText, FormatTags(request.Keywords));
                    }
                    else
                    {
                        text = FormatText(request.Title, Constants.LinkText, FormatTags(request.Keywords));
                    }
                }
                else
                {
                    if (titleLinkLenght + Constants.IntroductoryText.Length < characterLimit)
                    {
                        text = FormatText(Constants.IntroductoryText + request.Title, Constants.LinkText);
                    }
                    else
                    {
                        if (titleLinkLenght < characterLimit)
                        {
                            text = FormatText(request.Title, Constants.LinkText);
                        }
                        else
                        {
                            text = Constants.LongTitleText;
                        }
                    }
                }
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

        private static string FormatText(string title, string description, string? tags = null)
        {
            return $"""
                {title}
                {description}{tags}
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
