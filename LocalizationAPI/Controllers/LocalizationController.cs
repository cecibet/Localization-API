using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace YourApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string lang)
        {
            string filePath = $"Translations/{lang}/t.json";

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            string fileContents = System.IO.File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var json = JsonSerializer.Deserialize<object>(fileContents, options);
            return Ok(json);
        }

        [HttpPost]
        public IActionResult Update(string lang, [FromBody] JsonElement newJson)
        {
            string filePath = $"Translations/{lang}/t.json";

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            string newJsonString = JsonSerializer.Serialize(newJson, options);
            System.IO.File.WriteAllText(filePath, newJsonString);
            return Ok();
        }
    }
}