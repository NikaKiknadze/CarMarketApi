using CarMarketApi.Dtos.BuyerDtos;
using CarMarketApi.Dtos;
using CarMarketApi.Service.AllServices;
using CarMarketApi.Service.ServiceAbstracts;
using Microsoft.AspNetCore.Mvc;
using CarMarketApi.Dtos.ItemDtos;

namespace CarMarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("Items", Name = "GetItems")]
        public async Task<ActionResult<GetDtosWithCount<ItemGetDto>>> GetItemsAsync([FromQuery] ItemFilterDto filter, CancellationToken cancellationToken)
        {
            var result = await _itemService.GetItemsAsync(filter, cancellationToken);
            return Ok(result);
        }

        [HttpPost("Items", Name = "PostItem")]
        public async Task<ActionResult<BuyerGetDto>> PostItemAsync(ItemPostDto input, CancellationToken cancellationToken)
        {
            var result = await _itemService.CreateItemAsync(input, cancellationToken);
            return Ok(result);
        }

        [HttpPut("Items", Name = "PutItem")]
        public async Task<ActionResult<bool>> PutItemAsync(ItemPutDto input, CancellationToken cancellationToken)
        {
            var result = await _itemService.UpdateItemAsync(input, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("Items/{itemId}", Name = "DeleteItem")]
        public async Task<ActionResult<bool>> DeleteItemAsync(int itemId, CancellationToken cancellationToken)
        {
            var result = await _itemService.DeleteItemAsync(itemId, cancellationToken);
            return Ok(result);
        }
    }
}
