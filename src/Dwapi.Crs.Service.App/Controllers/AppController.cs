using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Command;
using Dwapi.Crs.Core.Domain.Dto;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.Core.Interfaces.Service;
using Dwapi.Crs.Service.Application.Commands;
using Dwapi.Crs.Service.Application.Queries;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "UpiManager")]
        [HttpPost("Generate")]
        public async Task<IActionResult> Generate()
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
        [Authorize(Roles = "UpiManager")]
        [HttpPost("DumpAll")]
        public async Task<IActionResult> Dump()
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

        
        [Authorize(Roles = "UpiManager")]
        [HttpPost("DumpSite")]
        public async Task<IActionResult> DumpSingle([FromBody] SiteDto siteDto)
        {
            try
            {
                await  _mediator.Send(new DumpClientsBySite(siteDto.SiteCodes));
                return Ok(siteDto.SiteCodes);
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
                    version=$"{GetType().Assembly.GetName().Version}",
                    build = "25MAY220706"
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "status error");
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet("Report")]
        public async Task<IActionResult> Report()
        {
            try
            {
                var result = await _mediator.Send(new GetTheReport());
                if (result.IsSuccess)
                    return Ok(result.Value);

                throw new Exception(result.Error);
            }
            catch (Exception e)
            {
                Log.Error(e, "report error");
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet("PendingReport")]
        public async Task<IActionResult> PendingReport()
        {
            try
            {
                var result = await _mediator.Send(new GetTheReport());
                if (result.IsSuccess)
                    return Ok(result.Value.Where(x=>x.IsPending));

                throw new Exception(result.Error);
            }
            catch (Exception e)
            {
                Log.Error(e, "report error");
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet("TransmissionReport")]
        public async Task<IActionResult> TransmissionReport()
        {
            try
            {
                var result = await _mediator.Send(new GetTheReport());
                if (result.IsSuccess)
                    return Ok(result.Value.Where(x=>x.IsTransmitted));

                throw new Exception(result.Error);
            }
            catch (Exception e)
            {
                Log.Error(e, "report error");
                return StatusCode(500, e.Message);
            }
        }
          
        [HttpGet("ErrorReport/{siteCode}")]
        public async Task<IActionResult> ErrorReport(int siteCode)
        {
            try
            {
                var result = await _mediator.Send(new GetErrorReport(siteCode));
                if (result.IsSuccess)
                    return Ok(result.Value);

                throw new Exception(result.Error);
            }
            catch (Exception e)
            {
                Log.Error(e, "report error");
                return StatusCode(500, e.Message);
            }
        }
    }
}
