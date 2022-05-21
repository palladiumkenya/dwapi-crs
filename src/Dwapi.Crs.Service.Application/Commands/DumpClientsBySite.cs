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
    public class DumpClientsBySite : IRequest<Result>
    {
        public int[] SiteCodes { get; }

        public DumpClientsBySite(int[] siteCodes)
        {
            SiteCodes = siteCodes;
        }
    }

    public class DumpClientsBySiteHandler:IRequestHandler<DumpClientsBySite,Result>
    {
        private readonly IMediator _mediator;
        private readonly CrsSettings _crsSettings;
        private readonly IMapper _mapper;
        private readonly ICrsDumpService _crsDumpService;
        private readonly IRegistryManifestRepository _manifestRepository;
        private readonly IClientRepository _clientRepository;

        public DumpClientsBySiteHandler(IMediator mediator, CrsSettings crsSettings, IMapper mapper,
            ICrsDumpService crsDumpService, IRegistryManifestRepository manifestRepository,
            IClientRepository clientRepository)
        {
            _mediator = mediator;
            _crsSettings = crsSettings;
            _mapper = mapper;
            _crsDumpService = crsDumpService;
            _manifestRepository = manifestRepository;
            _clientRepository = clientRepository;
        }

        public async Task<Result> Handle(DumpClientsBySite request, CancellationToken cancellationToken)
        {
            Log.Debug("checking for available manifests");
            var manis =await  _manifestRepository.GetReadyForSending(request.SiteCodes);
            if (manis.Any())
            {
                Log.Debug($"{manis.Count} Manifests Available");
                
                foreach (var mani in manis)
                {
                    var pageCount = Pager.PageCount(_crsSettings.Batches, mani.Records.Value);

                    for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                    {
                        Log.Debug($"sending {mani.Name} {pageNumber} of {pageCount}");
                        var clients = _clientRepository.Load(pageNumber, _crsSettings.Batches, mani.FacilityId);
                        var dtos = _mapper.Map<List<ClientExchange>>(clients);
                        var res = await _crsDumpService.Dump(dtos);
                        var responseInfo = $"Page:{pageNumber}/{pageCount}, Clients:{dtos.Count}, Response:{res.Response}";
                        await _mediator.Publish(new SiteDumped(mani.Id,res.StatusCode,responseInfo));
                        Log.Debug(new string('-', 50));
                        Log.Debug(res.Response);
                        Log.Debug(new string('^', 50));
                        Log.Debug($"SENT {mani.Name} [{pageNumber} of {pageCount}]");
                    }
                }
                
            }
            else
            {
                Log.Debug("NO manifests found");
            }

            return Result.Ok();
        }
    }
}
