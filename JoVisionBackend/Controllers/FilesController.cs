using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create(
            IFormFile file,
            [FromForm] string owner)
        {
            if (file == null || string.IsNullOrEmpty(owner))
            {
                return BadRequest();
            }

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(
                uploadsFolder,
                file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("Created");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(
            [FromQuery] string fileName)
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads",
                fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return BadRequest();
            }

            System.IO.File.Delete(filePath);

            return Ok("Deleted");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(
            IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }

            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads");

            var filePath = Path.Combine(
                uploadsFolder,
                file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("Updated");
        }

        [HttpGet("Retrieve")]
        public IActionResult Retrieve(
            [FromQuery] string fileName)
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads",
                fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return BadRequest();
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);

            return File(bytes, "image/jpeg", fileName);
        }
    }
}