using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<string> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return "No file uploaded";
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

            return $"File uploaded successfully: {file.FileName}";
        }
    }
}