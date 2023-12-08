using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlyBot_Business.IRepository;
using OnlyBot_Models;
using OnlyBot_Models.APIResponse;

namespace OnlyBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScriptController : ControllerBase
    {
        private readonly IScriptRepository _scriptRepository;

        public ScriptController(IScriptRepository scriptRepository)
        {
            _scriptRepository = scriptRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _scriptRepository.GetAll());
        }

        [HttpGet("{scriptId}")]
        public async Task<IActionResult> Get(Guid scriptId)
        {
            if (scriptId == Guid.Empty)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var script = await _scriptRepository.Get(scriptId);
            if (script == null)
            {
                return NotFound(new ErrorModel()
                {
                    ErrorMessage = "Script not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(script);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Script Script)
        {
            if (Script == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid script data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var createdScript = await _scriptRepository.Create(Script);

            return CreatedAtAction(nameof(Get), new { scriptId = createdScript.Id }, createdScript);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Script Script)
        {
            if (Script == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid script data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var updatedScript = await _scriptRepository.Update(Script);

            return Ok(updatedScript);
        }

        [HttpDelete("{scriptId}")]
        public async Task<IActionResult> Delete(Guid scriptId)
        {
            if (scriptId == Guid.Empty)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var deletedScript = await _scriptRepository.Delete(scriptId);

            return Ok(deletedScript);
        }
    }
}
