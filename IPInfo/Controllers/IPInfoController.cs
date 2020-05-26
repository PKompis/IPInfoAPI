using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire;
using IPInfo.Core.Services;
using IPInfo.Library.Interfaces;
using IPInfo.Resources;
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

        /// <summary>
        /// Get IP Details
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>Retrieves IP Information</returns>
        [HttpGet("{ip}")]
        public async Task<ActionResult<IPDetails>> GetIPDetails(string ip)
        {
            var ipResponse = await _ipService.GetIPDetails(ip);

            return Ok(ipResponse);
        }

        /// <summary>
        /// Get Batch Progress
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retrieves an string which refers to the completion percentage</returns>
        [HttpGet("batch/{id}/progress")]
        public ActionResult<TrackResponseProgressResponse> GetBatchProgress(Guid? id)
        {
            var result = _ipService.GetBatchProgress(id);

            return Ok(new TrackResponseProgressResponse(result));
        }

        /// <summary>
        /// Post Multiple IPs
        /// </summary>
        /// <param name="ipDetailsList"></param>
        /// <returns>Retrieves Guid for monitoring the progress</returns>
        [HttpPost("batch")]
        public ActionResult<TrackResponseIdentifierResource> PostIpDetailsList([FromBody] IEnumerable<SaveIPDetails> ipDetailsList)
        {
            var trackingGuid = Guid.NewGuid();

            BackgroundJob.Enqueue<IIPService>(x => _ipService.PostIpDetailsList(ipDetailsList, trackingGuid));

            return Ok(new TrackResponseIdentifierResource(trackingGuid));
        }
    }
}
