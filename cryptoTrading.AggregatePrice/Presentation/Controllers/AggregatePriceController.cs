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
    }
}
