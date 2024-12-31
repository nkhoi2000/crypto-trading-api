using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cryptoTrading.TradingService.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingController : ControllerBase
    {
        [HttpGet("Health")]
        public IActionResult Health()
        {
            return Ok("Ok");
        }
    }
}
