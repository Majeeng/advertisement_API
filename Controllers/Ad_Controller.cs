using Microsoft.AspNetCore.Mvc;

namespace advertisement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdPlatformController : ControllerBase
    {
        private static readonly AdPlatform_Repository _repository = new AdPlatform_Repository();
        private readonly ILogger<AdPlatformController> _logger;

        public AdPlatformController(ILogger<AdPlatformController> logger)
        {
            _logger = logger;
        }

        [HttpPost("load")]
        public IActionResult LoadAdPlatforms([FromBody] string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                _logger.LogWarning("Пустой путь к файлу");
                return BadRequest("Путь к файлу не может быть пустым");
            }

            try
            {
                _repository.LoadFromFile(filePath);
                _logger.LogInformation($"Загрузка успешно выполнена! Путь к файлу: {filePath}");
                return Ok("Data loaded");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при загрузке данных из файла: {filePath}");
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public IActionResult SearchPlatforms(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                _logger.LogWarning("Пустое поле названия локации");
                return BadRequest("Поле локации не может быть пустым");
            }

            var platforms = _repository.GetPlatformsForLocation(location);
            _logger.LogInformation($"Поиск по локации: {location} успешно выполнен. Найдено {location.Count()} рекламных площадок.");
            return Ok(platforms);
        }
    }
}