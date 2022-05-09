using System;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Command;
using Dwapi.Crs.Core.Domain.Dto;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.Core.Interfaces.Service;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Crs.Controllers
{
    [Route("api/[controller]")]
    public class CrsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestService _manifestService;
        private readonly ICrsService _crsService;

        public CrsController(IMediator mediator, IManifestRepository manifestRepository,
            IManifestService manifestService, ICrsService crsService)
        {
            _mediator = mediator;
            _manifestService = manifestService;
            _crsService = crsService;
        }

        // POST api/Crs/verify
        [HttpPost("Verify")]
        public async Task<IActionResult> Verify([FromBody] SubscriberDto subscriber)
        {
            if (null == subscriber)
                return BadRequest();

            try
            {
                var dockect = await _mediator.Send(new VerifySubscriber(subscriber.SubscriberId,subscriber.AuthToken), HttpContext.RequestAborted);
                return Ok(dockect);
            }
            catch (Exception e)
            {
                Log.Error(e, "verify error");
                return StatusCode(500, e.Message);
            }
        }

        // POST api/Crs/Manifest
        [HttpPost("Manifest")]
        public async Task<IActionResult> ProcessManifest([FromBody] ManifestExtractDto manifestDto)
        {
            if (null == manifestDto)
                return BadRequest();
            try
            {
                var manifest = new SaveManifest(manifestDto.Manifest);
                manifest.AllowSnapshot = Startup.AllowSnapshot;
                var faciliyKey = await _mediator.Send(manifest, HttpContext.RequestAborted);
                BackgroundJob.Enqueue(() => _manifestService.Process(manifest.Manifest.SiteCode));
                return Ok(new
                {
                    FacilityKey = faciliyKey
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("ClientRegistry")]
        public IActionResult ProcessClientRegistry([FromBody] CrsExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Enqueue(() => _crsService.Process(extract.CrsExtracts));
                return Ok(new {BatchKey = id});
            }
            catch (Exception e)
            {
                Log.Error(e, "ClientRegistry error");
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet("Status")]
        public IActionResult GetStatus()
        {
            try
            {
                var ver = GetType().Assembly.GetName().Version;
                return Ok(new
                {
                    name = "Dwapi Central - API (CRS)",
                    status = "running",
                    build = "09MAY221650"
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "status error");
                return StatusCode(500, e.Message);
            }
        }
    }
}
