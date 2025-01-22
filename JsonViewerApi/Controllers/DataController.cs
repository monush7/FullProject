using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.json");

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        if (!System.IO.File.Exists(_filePath))
            return NotFound("Data file not found.");

        var jsonData = await System.IO.File.ReadAllTextAsync(_filePath);
        return Ok(JsonSerializer.Deserialize<object>(jsonData));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] object data)
    {
        await System.IO.File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(data));
        return Ok("Data updated successfully.");
    }
}
