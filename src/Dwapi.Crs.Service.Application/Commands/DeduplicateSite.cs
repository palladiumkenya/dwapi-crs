using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Dwapi.Crs.Core.Exchange;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Domain.Dtos;
using Dwapi.Crs.Service.Application.Events;
using Dwapi.Crs.Service.Application.Interfaces;
using Dwapi.Crs.SharedKernel.Custom;
using Dwapi.Crs.SharedKernel.Enums;
using MediatR;
using Serilog;

namespace Dwapi.Crs.Service.Application.Commands
{
    public class DeduplicateSite : IRequest<Result>
    {
        public List<SiteDuplicateDto> Sites { get; }

        public DeduplicateSite(List<SiteDuplicateDto> sites)
        {
            Sites = sites;
        }
    }

    public class DeduplicateSiteHandler:IRequestHandler<DeduplicateSite,Result>
    {
        private readonly IMapper _mapper;
        private IMediator _mediator;
        private readonly IClientRepository _clientRepository;
        private readonly IRegistryManifestRepository _registryManifestRepository;
        private readonly IProgress<AppProgress> _progress;

        public DeduplicateSiteHandler(IMapper mapper, IMediator mediator, IClientRepository clientRepository, IRegistryManifestRepository registryManifestRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _clientRepository = clientRepository;
            _registryManifestRepository = registryManifestRepository;
            
            var progress = new Progress<AppProgress>();
            progress.ProgressChanged += async (sender, appProgress) =>
            {
                await _mediator.Publish(new AppProgressReported(appProgress));
            };
            _progress = progress;
        }

        public async Task<Result> Handle(DeduplicateSite request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Debug("Deduplicating");

                var appProgress = AppProgress.New(Area.Deduplicating,"Deduplicating...", 0);
                _progress.Report(appProgress);
                int i = 0;
                foreach (var site in request.Sites)
                {
                    i++;
                    Log.Debug($"Deduplicating {site.Name},Number of Duplicates:{site.PatientPks}");
                    
                    appProgress.Update($"Deduplicating {site.Name}...");
                    _progress.Report(appProgress);
                    
                    await _clientRepository.DeDuplicate(site);
                    await _registryManifestRepository.ReProcess(site.SiteCode);
                    
                    appProgress.Update($"Deduplicating {site.Name}",i,request.Sites.Count);
                    _progress.Report(appProgress);
                }
                
                appProgress.UpdateDone($"Deduplication for [{request.Sites.Count}] Sites Completed !");
                _progress.Report(appProgress);
                
                Log.Debug("Deduplicating completed");
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
