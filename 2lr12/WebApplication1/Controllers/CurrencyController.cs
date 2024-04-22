using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Hubs;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController(
        IHubContext<CurrencyHub> currencyHubContext, 
        ICurrencyService currencyService
        ) : ControllerBase
    {
        private readonly IHubContext<CurrencyHub> _currencyHubContext = currencyHubContext;
        private readonly ICurrencyService _currencyService = currencyService;

        [HttpGet("update")]
        public async Task<IActionResult> UpdateCurrencyRates()
        {
            var rates = await _currencyService.GetCurrencyRates();
            await _currencyHubContext.Clients.All.SendAsync("ReceiveCurrencyRates", rates);

            return Ok(new { status = "Data sent to clients." });
        }
    }
}
