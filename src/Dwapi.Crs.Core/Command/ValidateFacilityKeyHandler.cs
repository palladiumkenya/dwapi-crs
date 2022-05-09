using System;
using System.Threading;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Interfaces.Repository;
using Dwapi.Crs.SharedKernel.Exceptions;
using MediatR;

namespace Dwapi.Crs.Core.Command
{

    public class ValidateFacilityKey : IRequest<bool>
    {
        public Guid Key { get; }

        public ValidateFacilityKey(Guid key)
        {
            Key = key;
        }
    }
    public class ValidateFacilityKeyHandler: IRequestHandler<ValidateFacilityKey,bool>
    {
        private readonly IFacilityRepository _repository;

        public ValidateFacilityKeyHandler(IFacilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ValidateFacilityKey request, CancellationToken cancellationToken)
        {
            var masterFacility =await _repository.GetAsync(x=>x.Id==request.Key);

            if (null==masterFacility)
                throw new FacilityNotFoundException(request.Key);

            return true;
        }
    }
}
