using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Domain.Dtos;
using Dwapi.Crs.Service.Application.Interfaces;
using MediatR;
using Serilog;

namespace Dwapi.Crs.Service.Application.Queries
{
    public class GetDuplicateSummary : IRequest<Result<List<SiteDuplicateDto>>>
    {
    }

    public class GetDuplicateSummaryHandler : IRequestHandler<GetDuplicateSummary, Result<List<SiteDuplicateDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IClientRepository _repository;

        public GetDuplicateSummaryHandler(IMapper mapper, IClientRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task<Result<List<SiteDuplicateDto>>> Handle(GetDuplicateSummary request, CancellationToken cancellationToken)
        {
            try
            {
                var report =  _repository.LoadDuplicateSummary();
                return Task.FromResult(Result.Ok(report));
            }
            catch (Exception e)
            {
                Log.Error(e,"Report Error");
                   return Task.FromResult(Result.Fail<List<SiteDuplicateDto>>(e.Message));
            }
        }
    }

}