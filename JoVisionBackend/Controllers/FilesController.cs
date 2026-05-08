using Microsoft.AspNetCore.Mvc;
using JoVisionBackend.Models;

namespace JoVisionBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private static List<ImageData> images = new();

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

            images.Add(new ImageData
            {
                FileName = file.FileName,
                Owner = owner
            });

            return Ok("Created");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(
            [FromQuery] string fileName,
            [FromQuery] string owner)
        {
            var image = images.FirstOrDefault(x =>
                x.FileName == fileName &&
                x.Owner == owner);

            if (image == null)
            {
                return BadRequest();
            }

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Uploads",
                fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            images.Remove(image);

            return Ok("Deleted");
        }
    }
}