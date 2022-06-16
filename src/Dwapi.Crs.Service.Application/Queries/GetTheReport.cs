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
    public class GetTheReport : IRequest<Result<List<TheReportDto>>>
    {

        public ReportState State { get; }

        public GetTheReport(ReportState state)
        {
            State = state;
        }
    }

    public class GetTheReportHandler : IRequestHandler<GetTheReport, Result<List<TheReportDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRegistryManifestRepository _repository;

        public GetTheReportHandler(IMapper mapper, IRegistryManifestRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<List<TheReportDto>>> Handle(GetTheReport request, CancellationToken cancellationToken)
        {
            try
            {
                var report = await _repository.GetTheReport();
                
                if (request.State == ReportState.Pending)
                {
                    var alreadySentSiteCodes = report.Where(x => x.IsAlreadySent).Select(s => s.SiteCode).ToList();
                    report = report.Where(x => !alreadySentSiteCodes.Contains(x.SiteCode)).ToList();
                }
                
                if (request.State == ReportState.Failed)
                {
                    report = report.Where(x => x.IsFailed).ToList();
                }

                return Result.Ok(report);
            }
            catch (Exception e)
            {
                Log.Error(e,"Report Error");
                   return Result.Fail<List<TheReportDto>>(e.Message);
            }
        }
    }

}