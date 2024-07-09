using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlyBot_Business.IRepository;
using OnlyBot_Models;
using OnlyBot_Models.APIResponse;

namespace OnlyBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _inventoryRepository.GetAll());
        }

        [HttpGet("{iventoryId}")]
        public async Task<IActionResult> Get(Guid inventoryId)
        {
            if (inventoryId == Guid.Empty)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var inventory = await _inventoryRepository.Get(inventoryId);
            if (inventory == null)
            {
                return NotFound(new ErrorModel()
                {
                    ErrorMessage = "Inventory not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Inventory Inventory)
        {
            if (Inventory == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid inventory data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var inventory = await _inventoryRepository.Create(Inventory);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Inventory Inventory)
        {
            if (Inventory == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid inventory data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var inventory = await _inventoryRepository.Update(Inventory);

            return Ok(inventory);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid inventoryId)
        {
            if (inventoryId == Guid.Empty)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var result = await _inventoryRepository.Delete(inventoryId);

            if (result == 0)
            {
                return NotFound(new ErrorModel()
                {
                    ErrorMessage = "Inventory not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok();
        }
    }   
}
