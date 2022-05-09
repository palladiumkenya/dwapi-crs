using System;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Core.Interfaces.Repository;
using MediatR;

namespace Dwapi.Crs.Core.Command
{
    public class SaveManifest : IRequest<Guid>
    {
        public Manifest Manifest { get; set; }
        public bool AllowSnapshot { get; set; }

        public SaveManifest()
        {
        }

        public SaveManifest(Manifest manifest)
        {
            Manifest = manifest;
        }
    }
    public class SaveManifestHandler : IRequestHandler<SaveManifest, Guid>
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _repository;

        public SaveManifestHandler(IMediator mediator, IManifestRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<Guid> Handle(SaveManifest request, CancellationToken cancellationToken)
        {
            var facilityId = await _mediator.Send(new EnrollFacility(request.Manifest.SiteCode,request.Manifest.Name,request.Manifest.EmrName), cancellationToken);

            request.Manifest.UpdateFacility(facilityId);
            _repository.Create(request.Manifest);
            await _repository.SaveAsync();


            return request.Manifest.Id;
        }
    }
}
