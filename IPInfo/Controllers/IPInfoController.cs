using System.Threading.Tasks;
using IPInfo.Core.Services;
using IPInfo.Library.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IPInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IPInfoController : ControllerBase
    {
        private readonly IIPService _ipService;

        public IPInfoController(IIPService ipService)
        {
            _ipService = ipService;
        }

        [HttpGet("{ip}")]
        public async Task<ActionResult<IPDetails>> GetIPDetails(string ip)
        {
            var ipResponse = await _ipService.GetIPDetails(ip);

            return Ok(ipResponse);
        }
    }
}
