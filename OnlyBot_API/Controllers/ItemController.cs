using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlyBot_Business.IRepository;
using OnlyBot_Models;
using OnlyBot_Models.APIResponse;


namespace OnlyBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _itemRepository.GetAll());
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> Get(Guid itemId)
        {
            if (itemId == Guid.Empty)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var item = await _itemRepository.Get(itemId);
            if (item == null)
            {
                return NotFound(new ErrorModel()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Item Item)
        {
            if (Item == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid item data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var item = await _itemRepository.Create(Item);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Item Item)
        {
            if (Item == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid item data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var item = await _itemRepository.Update(Item);

            return Ok(item);
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> Delete(Guid itemId)
        {
            if (itemId == Guid.Empty)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var result = await _itemRepository.Delete(itemId);
            if (result == 0)
            {
                return NotFound(new ErrorModel()
                {
                    ErrorMessage = "Item not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok();
        }
    }
}
