using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Interfaces;
using MediatR;
using Serilog;

namespace Dwapi.Crs.Service.Application.Queries
{
    public class GetSiteReport:IRequest<Result<List<SiteReportDto>>>
    {
    }

    public class GetSiteReportHandler : IRequestHandler<GetSiteReport, Result<List<SiteReportDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRegistryManifestRepository _repository;

        public GetSiteReportHandler(IMapper mapper, IRegistryManifestRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<List<SiteReportDto>>> Handle(GetSiteReport request, CancellationToken cancellationToken)
        {
            try
            {
                var report = await _repository.GetSiteReport();
                return Result.Ok(report);
            }
            catch (Exception e)
            {
                Log.Error(e,"Report Error");
                   return Result.Fail<List<SiteReportDto>>(e.Message);
            }
        }
    }

}