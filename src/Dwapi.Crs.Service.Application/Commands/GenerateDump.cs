using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Dwapi.Crs.Core.Exchange;
using Dwapi.Crs.Service.Application.Domain;
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

        public GenerateDumpHandler(CrsSettings crsSettings, IMapper mapper, ICrsDumpService crsDumpService, IRegistryManifestRepository manifestRepository, IClientRepository clientRepository)
        {
            _crsSettings = crsSettings;
            _mapper = mapper;
            _crsDumpService = crsDumpService;
            _manifestRepository = manifestRepository;
            _clientRepository = clientRepository;
        }

        public async Task<Result> Handle(GenerateDump request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Debug("Generating dump...");
                await _manifestRepository.Generate();
                Log.Debug("Dump completed!");
                Log.Debug("Generating Processing counts...");
                await _manifestRepository.Process();
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
