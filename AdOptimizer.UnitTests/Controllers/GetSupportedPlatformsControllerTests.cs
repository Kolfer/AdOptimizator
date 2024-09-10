using AdOptimizer.Controllers.GetSupportedPlatforms;
using AdOptimizer.Models.GetSupportedPlatforms;
using AdOptimizer.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AdOptimizer.UnitTests.Controllers
{
    public class GetSupportedPlatformsControllerTests
    {
        private readonly GetSupportedPlatformsController _controller;

        public GetSupportedPlatformsControllerTests()
        {
            _controller = new GetSupportedPlatformsController();
        }

        [Fact]
        public void GetSupportedPlatforms_NegativeCharacterLimit_ReturnBadRequest()
        {
            var request = new GetSupportedPlatformsRequest()
            {
                CharacterLimit = -10
            };

            var result = _controller.GetSupportedPlatforms(request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal(Errors.NegativeCharacterLimit, badRequestResult.Value);
        }

        [Fact]
        public void GetSupportedPlatforms_SmallCharacterLimit_ReturnNotFound()
        {
            var characterLimit = 1;

            var request = new GetSupportedPlatformsRequest()
            {
                CharacterLimit = characterLimit
            };

            var result = _controller.GetSupportedPlatforms(request);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal(string.Format(Errors.NoPlatformFitsCharacterLimit, characterLimit), notFoundResult.Value);
        }

        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(0)]
        public void GetSupportedPlatforms_CorrectCharacterLimit_ReturnFullList(int characterLimit)
        {
            var request = new GetSupportedPlatformsRequest()
            {
                CharacterLimit = characterLimit
            };

            var result = _controller.GetSupportedPlatforms(request);

            var getSupportedPlatformsResult = Assert.IsType<GetSupportedPlatformsResponse>(result.Value);
            Assert.Equal([..Constants.SocialMedia], getSupportedPlatformsResult.SupportedPlatforms);
        }
    }
}
