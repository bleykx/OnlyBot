using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlyBot_Business;
using OnlyBot_Business.IRepository;
using OnlyBot_Models;
using OnlyBot_Models.APIResponse;
using OnlyBot_Models.Enums;

namespace OnlyBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IBotRepository _botRepository;
        public BotController (IBotRepository botRepository)
        {
            _botRepository = botRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _botRepository.GetAll());

        }

        [HttpGet("{botId}")]
        public async Task<IActionResult> Get(Guid botId)
        {
            if (botId == Guid.Empty)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage= "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var bot = await _botRepository.Get(botId);
            if(bot == null)
            {
                return NotFound(new ErrorModel()
                {
                    ErrorMessage = "Bot not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(bot);
        }

        [HttpGet("server/{serverName}")]
        public async Task<IActionResult> GetAllBotsByServer(string serverName)
        {
            var serverEnum = EnumHelper.GetServerEnumFromString(serverName);

            var botsByServer = await _botRepository.GetAll();

            return Ok(botsByServer.Where(bot => bot.Server == serverEnum));          
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Bot Bot)
        {
            if (Bot == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid bot data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var bot = await _botRepository.Create(Bot);

            return CreatedAtAction(nameof(Get), new { botId = bot.Id }, bot);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Bot Bot)
        {
            if (Bot == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid bot data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var bot = await _botRepository.Update(Bot);

            return Ok(bot);
        }

        [HttpDelete("{botId}")]
        public async Task<IActionResult> Delete(Guid botId)
        {
            if (botId == Guid.Empty)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var result = await _botRepository.Delete(botId);

            if(result == 0)
            {
                return NotFound(new ErrorModel()
                {
                    ErrorMessage = "Bot not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok();
        }

        [HttpPost("filteredbots")]
        public async Task<IActionResult> GetFilteredBots(Dictionary<string, List<string>> filters)
        {
            var bots = await _botRepository.GetFilteredBots(filters);

            return Ok(bots);
        }
    }
}
