using CarMarketApi.Dtos.BuyerDtos;
using CarMarketApi.Dtos;
using CarMarketApi.Service.AllServices;
using CarMarketApi.Service.ServiceAbstracts;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : Controller
    {
        private readonly IBuyerService _buyerServices;
        public BuyerController(IBuyerService buyerService)
        {
            _buyerServices = buyerService;
        }

        [HttpGet("Buyers", Name = "GetBuyers")]
        public async Task<ActionResult<GetDtosWithCount<BuyerGetDto>>> GetBuyersAsync([FromQuery] BuyerFilterDto filter, CancellationToken cancellationToken)
        {
            var result = await _buyerServices.GetBuyersAsync(filter, cancellationToken);
            return Ok(result);
        }

        [HttpPost("Buyers", Name = "PostBuyer")]
        public async Task<ActionResult<BuyerGetDto>> PostBuyerAsync(BuyerPostDto input, CancellationToken cancellationToken)
        {
            var result = await _buyerServices.CrreateBuyerAsync(input, cancellationToken);
            return Ok(result);
        }

        [HttpPut("Buyers", Name = "PutBuyer")]
        public async Task<ActionResult<bool>> PutUserAsync(BuyerPutDto input, CancellationToken cancellationToken)
        {
            var result = await _buyerServices.UpdateBuyerAsync(input, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("Buyers/{buyerId}", Name = "DeleteBuyer")]
        public async Task<ActionResult<bool>> DeleteBuyerAsync(int buyerId, CancellationToken cancellationToken)
        {
            var result = await _buyerServices.DeleteBuyerAsync(buyerId, cancellationToken);
            return Ok(result);
        }
    }
}
