using CarMarketApi.Dtos.BuyerDtos;
using CarMarketApi.Dtos;
using CarMarketApi.Service.AllServices;
using CarMarketApi.Service.ServiceAbstracts;
using Microsoft.AspNetCore.Mvc;
using CarMarketApi.Dtos.SellerDtos;

namespace CarMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : Controller
    {
        private readonly ISellerService _sellerService;
        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet("Sellers", Name = "GetSellers")]
        public async Task<ActionResult<GetDtosWithCount<SellerGetDto>>> GetSellersAsync([FromQuery] SellerFilterDto filter, CancellationToken cancellationToken)
        {
            var result = await _sellerService.GetSellersAsync(filter, cancellationToken);
            return Ok(result);
        }

        [HttpPost("Sellers", Name = "PostSeller")]
        public async Task<ActionResult<SellerGetDto>> PostSellersAsync(SellerPostDto input, CancellationToken cancellationToken)
        {
            var result = await _sellerService.CreateSellerAsync(input, cancellationToken);
            return Ok(result);
        }

        [HttpPut("Sellers", Name = "PutSeller")]
        public async Task<ActionResult<bool>> PutSellerAsync(SellerPutDto input, CancellationToken cancellationToken)
        {
            var result = await _sellerService.UpdateSellerAsync(input, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("Sellers/{sellerId}", Name = "DeleteSeller")]
        public async Task<ActionResult<bool>> DeleteSellerAsync(int sellerId, CancellationToken cancellationToken)
        {
            var result = await _sellerService.DeleteSellerAsync(sellerId, cancellationToken);
            return Ok(result);
        }
    }
}
