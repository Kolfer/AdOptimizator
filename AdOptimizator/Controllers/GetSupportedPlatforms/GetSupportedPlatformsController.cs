using AdOptimizer.Models.GetSupportedPlatforms;
using AdOptimizer.Models.OptimizeAd;
using AdOptimizer.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AdOptimizer.Controllers.GetSupportedPlatforms
{
    [Route("api/")]
    public class GetSupportedPlatformsController : ControllerBase
    {
        [HttpGet("get-supported-platforms")]
        public ActionResult<GetSupportedPlatformsResponse> GetSupportedPlatforms(GetSupportedPlatformsRequest request)
        {
            if (Constants.SocialMedia.Count == 0)
            {
                return NotFound(Errors.EmptyPlatformList);
            }

            switch (request.CharacterLimit)
            {
                case > 0:
                    {
                        var supportedSocialMedia = new List<KeyValuePair<string, int>>();

                        foreach (var p in Constants.SocialMedia)
                        {
                            if (p.Value < request.CharacterLimit)
                            {
                                supportedSocialMedia.Add(p);
                            }
                        }

                        if (supportedSocialMedia.Count > 0)
                        {
                            return new GetSupportedPlatformsResponse()
                            {
                                SupportedPlatforms = supportedSocialMedia,
                            };
                        }
                        else
                        {
                            return NotFound(string.Format(Errors.NoPlatformFitsCharacterLimit, request.CharacterLimit));
                        }
                    } 
                case 0:
                    return new GetSupportedPlatformsResponse
                        {
                            SupportedPlatforms = [.. Constants.SocialMedia]
                        };
                default:
                    return BadRequest(Errors.NegativeCharacterLimit);
            }
        }
    }
}
