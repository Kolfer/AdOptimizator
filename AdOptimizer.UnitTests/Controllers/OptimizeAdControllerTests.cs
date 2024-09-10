using AdOptimizer.Controllers.OptimizeAd;
using AdOptimizer.Models.OptimizeAd;
using AdOptimizer.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AdOptimizer.UnitTests.Controllers
{
    public class OptimizeAdControllerTests
    {
        private readonly OptimizeAdController _controller;

        private const string _shortTitle = "title";
        private const string _shortDescription = "10 symbols";
        private const string _shortPlatform = "twitter";
        private const string _defaultKeyword = "keyword";

        private const string _longDescription = "loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong description";
        private const string _tooLongDescription = "too loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong  description";

        public OptimizeAdControllerTests()
        {
            _controller = new OptimizeAdController();
        }

        [Fact]
        public void OptimizeAd_EmptyRequest_ReturnsBadRequest()
        {
            var result = _controller.OptimizeAd(null);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal(Errors.EmptyRequest, badRequestResult.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void OptimizeAd_EmptyTitle_ReturnsBadRequest(string title)
        {
            var request = GetOptimizeAdRequest(title);

            var result = _controller.OptimizeAd(request);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(Errors.EmptyTitle, badRequestResult.Value);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void OptimizeAd_EmptyPlatform_ReturnsBadRequest(string platform)
        {
            var request = GetOptimizeAdRequest(_shortTitle, platform:platform);

            var result = _controller.OptimizeAd(request);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(Errors.EmptyPlatform, badRequestResult.Value);
        }

        [Fact]
        public void OptimizeAd_UnknownPlatform_ReturnsBadRequest()
        {
            var request = GetOptimizeAdRequest(platform: "UnknownPlatform");

            var result = _controller.OptimizeAd(request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal(Errors.UnknownPlatform, badRequestResult.Value);
        }

        [Fact]
        public void OptimizeAd_LongTitle_ReturnsBadRequest()
        {
            var request = GetOptimizeAdRequest(
                title: "loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong title",
                platform: "twitter");

            var result = _controller.OptimizeAd(request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal(Errors.TitleExceedsCharacterLimit, badRequestResult.Value);
        }

        [Theory]
        [InlineData("LINKEDIN")]
        [InlineData("linkedin")]
        [InlineData("twitter")]
        [InlineData("TWITTER")]
        public void OptimizeAd_DifferentPlatformCaseRegistry_ReturnsCorrectText(string platform)
        {
            var keywords = new List<string> { _defaultKeyword };
            var request = GetOptimizeAdRequest(_shortTitle, _shortDescription, keywords, platform);

            var result = _controller.OptimizeAd(request);

            var optimizedAdResponse = Assert.IsType<OptimizeAdResponse>(result.Value);
            Assert.Contains(_shortTitle, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_shortDescription, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_defaultKeyword, optimizedAdResponse.OptimizedAd);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        public void OptimizeAd_EmptyKeyword_ReturnsCorrectLength(string emptykeyword)
        {
            var keywords = new List<string> { _defaultKeyword, emptykeyword };
            var request = GetOptimizeAdRequest(_shortTitle, _shortDescription, keywords);
            
            var expectedContentLength = 
                Constants.IntroductoryText.Length + _shortTitle.Length + "\r\n".Length 
                + _shortDescription.Length + "\r\n".Length 
                + _defaultKeyword.Length + Constants.KeywordsText.Length;

            var result = _controller.OptimizeAd(request);

            var optimizedAdResponse = Assert.IsType<OptimizeAdResponse>(result.Value);
            Assert.Contains(_shortTitle, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_shortDescription, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_defaultKeyword, optimizedAdResponse.OptimizedAd);
            Assert.Equal(expectedContentLength, optimizedAdResponse.OptimizedAd?.Length);
        }

        [Fact]
        public void OptimizeAd_WithIntroTitleDescriptionKeywords_ReturnsCorrectText()
        {
            var keywords = new List<string> { _defaultKeyword };
            var request = GetOptimizeAdRequest(_shortTitle, _shortDescription, keywords, _shortPlatform);

            var result = _controller.OptimizeAd(request);

            var optimizedAdResponse = Assert.IsType<OptimizeAdResponse>(result.Value);
            Assert.Contains(Constants.IntroductoryText, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_shortTitle, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_shortDescription, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_defaultKeyword, optimizedAdResponse.OptimizedAd);
        }

        [Fact]
        public void OptimizeAd_WithIntroTitleDescriptionWithoutKeyWords_ReturnsCorrectText()
        {
            var request = GetOptimizeAdRequest(_shortTitle, _shortDescription, platform:_shortPlatform);

            var result = _controller.OptimizeAd(request);

            var optimizedAdResponse = Assert.IsType<OptimizeAdResponse>(result.Value);
            Assert.Contains(Constants.IntroductoryText, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_shortTitle, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_shortDescription, optimizedAdResponse.OptimizedAd);
        }

        [Fact]
        public void OptimizeAd_WithTitleDescriptionKeywordsWithoutIntro_ReturnsCorrectText()
        {
            var keywords = new List<string> { _defaultKeyword };
            var request = GetOptimizeAdRequest(_shortTitle, _longDescription, keywords, _shortPlatform);

            var result = _controller.OptimizeAd(request);

            var optimizedAdResponse = Assert.IsType<OptimizeAdResponse>(result.Value);
            Assert.DoesNotContain(Constants.IntroductoryText, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_shortTitle, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_defaultKeyword, optimizedAdResponse.OptimizedAd);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(_tooLongDescription)]
        public void OptimizeAd_WithIntroTitleLinkKeywords_ReturnsCorrectText(string description)
        {
            var keywords = new List<string> { _defaultKeyword };
            var request = GetOptimizeAdRequest(_shortTitle, description, keywords, _shortPlatform);

            var result = _controller.OptimizeAd(request);

            var optimizedAdResponse = Assert.IsType<OptimizeAdResponse>(result.Value);
            Assert.Contains(Constants.IntroductoryText, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_shortTitle, optimizedAdResponse.OptimizedAd);
            Assert.Contains(Constants.LinkText, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_defaultKeyword, optimizedAdResponse.OptimizedAd);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(_tooLongDescription)]
        public void OptimizeAd_WithIntroTitleLinkWithoutKeywords_ReturnsCorrectText(string description)
        {
            var request = GetOptimizeAdRequest(_shortTitle, description, platform:_shortPlatform);

            var result = _controller.OptimizeAd(request);

            var optimizedAdResponse = Assert.IsType<OptimizeAdResponse>(result.Value);
            Assert.Contains(Constants.IntroductoryText, optimizedAdResponse.OptimizedAd);
            Assert.Contains(Constants.LinkText, optimizedAdResponse.OptimizedAd);
            Assert.Contains(_shortTitle, optimizedAdResponse.OptimizedAd);
        }

        private OptimizeAdRequest GetOptimizeAdRequest(
            string title = "title",
            string description = "description",
            List<string> keywords = null,
            string platform = "LinkedIn")
        {
            return new OptimizeAdRequest
            {
                Title = title,
                Description = description,
                Keywords = keywords,
                Platform = platform
            };
        }
    }
}
