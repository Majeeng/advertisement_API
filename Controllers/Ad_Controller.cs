using Microsoft.AspNetCore.Mvc;

namespace advertisement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdPlatformController : ControllerBase
    {
        private static readonly AdPlatform_Repository _repository = new AdPlatform_Repository();

        [HttpPost("load")]
        public IActionResult LoadAdPlatforms([FromBody] string filePath)
        {
            try
            {
                _repository.LoadFromFile(filePath);
                return Ok("Data loaded");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("search")]
        public IActionResult SearchPlatforms(string location)
        {
            var platforms = _repository.GetPlatformsForLocation(location);
            return Ok(platforms);
        }
    }
}
