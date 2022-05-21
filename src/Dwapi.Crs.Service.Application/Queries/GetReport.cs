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
    public class GetReport:IRequest<Result<List<ReportDto>>>
    {
        public int[] SiteCodes { get; }

        public GetReport(int[] siteCodes)
        {
            SiteCodes = siteCodes;
        }
    }

    public class GetReportHandler : IRequestHandler<GetReport, Result<List<ReportDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRegistryManifestRepository _repository;

        public GetReportHandler(IMapper mapper, IRegistryManifestRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<List<ReportDto>>> Handle(GetReport request, CancellationToken cancellationToken)
        {
            try
            {
                var mani = await _repository.GetReport(request.SiteCodes);
                var report = _mapper.Map<List<ReportDto>>(mani);
                return Result.Ok(report);
            }
            catch (Exception e)
            {
                Log.Error(e, "Report Error");
                return Result.Fail<List<ReportDto>>(e.Message);
            }
        }
    }

}