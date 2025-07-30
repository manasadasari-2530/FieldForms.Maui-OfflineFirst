using Microsoft.AspNetCore.Mvc;

namespace OfflineFormsApi.Controllers
{
    [ApiController]
    [Route("api/forms")]
    public class FormsController : ControllerBase
    {
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var path = Path.Combine("Schemas", $"{name}.json");
            if (!System.IO.File.Exists(path))
                return NotFound();

            var json = System.IO.File.ReadAllText(path);
            return Content(json, "application/json");
        }
    }
}
