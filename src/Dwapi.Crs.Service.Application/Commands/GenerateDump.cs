using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Dwapi.Crs.Core.Exchange;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Events;
using Dwapi.Crs.Service.Application.Interfaces;
using Dwapi.Crs.SharedKernel.Custom;
using MediatR;
using Serilog;

namespace Dwapi.Crs.Service.Application.Commands
{
   public class GenerateDump:IRequest<Result>
    {
    }
    public class GenerateDumpHandler:IRequestHandler<GenerateDump,Result>
    {
        private readonly CrsSettings _crsSettings;
        private readonly IMapper _mapper;
        private readonly ICrsDumpService _crsDumpService;
        private readonly IRegistryManifestRepository _manifestRepository;
        private readonly IClientRepository _clientRepository;
        private IMediator _mediator;
        public GenerateDumpHandler(CrsSettings crsSettings, IMapper mapper, ICrsDumpService crsDumpService, IRegistryManifestRepository manifestRepository, IClientRepository clientRepository, IMediator mediator)
        {
            _crsSettings = crsSettings;
            _mapper = mapper;
            _crsDumpService = crsDumpService;
            _manifestRepository = manifestRepository;
            _clientRepository = clientRepository;
            _mediator = mediator;
        }

        public async Task<Result> Handle(GenerateDump request, CancellationToken cancellationToken)
        {
            var progress = new Progress<AppProgress>();
            progress.ProgressChanged += async (sender, appProgress) =>
            {
                await _mediator.Publish(new AppProgressReported(appProgress), cancellationToken);
            };
            
            try
            {
                Log.Debug("Generating dump...");
                var newSites = await _manifestRepository.Generate(progress);
                Log.Debug("Generating dump completed!");
                Log.Debug("Generating Processing counts...");
                await _manifestRepository.Process(progress);
                Log.Debug("Processing completed!");
                return Result.Ok();
                
            }
            catch (Exception e)
            {
                Log.Error(e,"Dump Error");
                return Result.Fail(e.Message);
            }
        }
    }
}
