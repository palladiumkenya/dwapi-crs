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
    public class DumpClientsBySite : IRequest<Result>
    {
        public int[] SiteCodes { get; }
        public bool Force  { get; }

        public DumpClientsBySite(int[] siteCodes,bool force=false)
        {
            SiteCodes = siteCodes.Distinct().ToArray();
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
        private readonly ITransmissionLogRepository _transmissionLogRepository;
        private readonly IProgress<AppProgress> _progress;

        public DumpClientsBySiteHandler(IMediator mediator, CrsSettings crsSettings, IMapper mapper,
            ICrsDumpService crsDumpService, IRegistryManifestRepository manifestRepository,
            IClientRepository clientRepository, ITransmissionLogRepository transmissionLogRepository)
        {
            _mediator = mediator;
            _crsSettings = crsSettings;
            _mapper = mapper;
            _crsDumpService = crsDumpService;
            _manifestRepository = manifestRepository;
            _clientRepository = clientRepository;
            _transmissionLogRepository = transmissionLogRepository;
            
            var progress = new Progress<AppProgress>();
            progress.ProgressChanged += async (sender, appProgress) =>
            {
                await _mediator.Publish(new AppProgressReported(appProgress));
            };
            _progress = progress;
        }

        public async Task<Result> Handle(DumpClientsBySite request, CancellationToken cancellationToken)
        {
            var appProgress = AppProgress.New("Transmitting...", 0);
            _progress.Report(appProgress);
            int i = 0;
            Log.Debug("checking for available manifests");
            var manis =await  _manifestRepository.GetReadyForSending(!request.Force, request.SiteCodes);
            if (manis.Any())
            {
                Log.Debug($"{manis.Count} Manifests Available");
                
                foreach (var mani in manis)
                {
                    i++;
                    // clear transmission Log
                    // await _transmissionLogRepository.Clear(mani.Id);
                    
                    var pageCount = Pager.PageCount(_crsSettings.Batches, mani.Records.Value);
                    
                    appProgress.Update($"Transmitting {mani.Name}");

                    for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
                    {
                        _progress.Report(appProgress);
                        Log.Debug($"Transmitting {mani.Name} {pageNumber} of {pageCount}");
                        var clients = _clientRepository.Load(pageNumber, _crsSettings.Batches, mani.FacilityId);
                        var dtos = _mapper.Map<List<ClientExchange>>(clients);
                        dtos = dtos.Where(x => x.IsValid()).ToList();
                        var res = await _crsDumpService.Dump(dtos);
                        
                        appProgress.Update($"Transmitting {mani.Name} Page:{pageNumber}/{pageCount}",i,manis.Count);
                        _progress.Report(appProgress);
                        
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
            
            appProgress.UpdateDone($"Transmitting {i} of {manis.Count} Site Manifests Done!");
            _progress.Report(appProgress);

            return Result.Ok();
        }
    }
}
