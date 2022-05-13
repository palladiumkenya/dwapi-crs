using System;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Command;
using Dwapi.Crs.Core.Domain.Dto;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.Core.Interfaces.Service;
using Dwapi.Crs.Service.Application.Commands;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Crs.Service.App.Controllers
{
    [Route("api/[controller]")]
    public class AppController : Controller
    {
        private readonly IMediator _mediator;

        public AppController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Generate")]
        public async Task<IActionResult> Generate([FromBody] SiteDto manifestDto)
        { 
            try
            {
                await _mediator.Send(new GenerateDump());
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost("DumpAll")]
        public async Task<IActionResult> Dump([FromBody] SiteDto manifestDto)
        {
            try
            {
                await _mediator.Send(new DumpClients());
                
                
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
                return StatusCode(500, e.Message);
            }
        }

        
        
        [HttpPost("DumpSite")]
        public async Task<IActionResult> DumpSingle([FromBody] SiteDto siteDto)
        {
            try
            {
                await  _mediator.Send(new DumpClientsBySite(siteDto.SiteCodes));
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
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
                    name = "Dwapi Central - API (CRS SERVICE APP)",
                    status = "running",
                    build = "13MAY221601"
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
