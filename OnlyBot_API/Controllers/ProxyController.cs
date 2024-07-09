using Microsoft.AspNetCore.Mvc;
using OnlyBot_Business;
using OnlyBot_Business.IRepository;
using OnlyBot_Models;
using OnlyBot_Models.APIResponse;

namespace OnlyBot_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProxyController : Controller
    {
        private readonly IProxyRepository _proxyRepository;

        public ProxyController(IProxyRepository proxyRepository)
        {
            _proxyRepository = proxyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _proxyRepository.GetAll());
        }

        [HttpGet("{proxyId}")]
        public async Task<IActionResult> Get(Guid proxyId)
        {
            if (proxyId == Guid.Empty)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var proxy = await _proxyRepository.Get(proxyId);
            if (proxy == null)
            {
                return NotFound(new ErrorModel()
                {
                    ErrorMessage = "Proxy not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(proxy);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Proxy Proxy)
        {
            if (Proxy == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid proxy data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var createdProxy = await _proxyRepository.Create(Proxy);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Proxy Proxy)
        {
            if (Proxy == null)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid proxy data",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            await _proxyRepository.Update(Proxy);
            return Ok();
        }

        [HttpDelete("{proxyId}")]
        public async Task<IActionResult> Delete(Guid proxyId)
        {
            if (proxyId == Guid.Empty)
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var result = await _proxyRepository.Delete(proxyId);

            if (result == 0)
            {
                return NotFound(new ErrorModel()
                {
                    ErrorMessage = "Proxy not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok();
        }
    }
}
