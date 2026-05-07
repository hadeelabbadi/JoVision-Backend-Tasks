using Microsoft.AspNetCore.Mvc;

namespace JoVisionBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BirthDatePostController : ControllerBase
    {
        [HttpPost]
        public string Post(
            [FromForm] string? name,
            [FromForm] int? years,
            [FromForm] int? months,
            [FromForm] int? days)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Hello anonymous";
            }

            if (years == null || months == null || days == null)
            {
                return $"Hello {name}, I can't calculate your age without knowing your birthdate";
            }

            DateTime birthDate;

            try
            {
                birthDate = new DateTime(
                    years.Value,
                    months.Value,
                    days.Value);
            }
            catch
            {
                return "Invalid birthdate";
            }

            int age = DateTime.Now.Year - birthDate.Year;

            if (DateTime.Now < birthDate.AddYears(age))
            {
                age--;
            }

            return $"Hello {name}, your age is {age}";
        }
    }
}