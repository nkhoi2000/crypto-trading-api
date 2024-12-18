using cryptoTrading.AggregatePrice.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cryptoTrading.AggregatePrice.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AggregatePriceController : ControllerBase
    {
        private readonly IPriceService _priceService;
        public AggregatePriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }
        [HttpPost("aggregate-price")]
        public async Task<IActionResult> AggregatePrices()
        {
            await _priceService.FetchAndAggregatePrices();
            return Ok("Prices aggregated successful");
        }
    }
}
