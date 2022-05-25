using System;
using System.Collections.Generic;
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
    public class GetErrorReport:IRequest<Result<ErrorReportDto>>
    {
        public int SiteCode { get; }

        public GetErrorReport(int siteCode)
        {
            SiteCode = siteCode;
        }
    }

    public class GetErrorReportHandler : IRequestHandler<GetErrorReport, Result<ErrorReportDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRegistryManifestRepository _repository;

        public GetErrorReportHandler(IMapper mapper, IRegistryManifestRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<ErrorReportDto>> Handle(GetErrorReport request, CancellationToken cancellationToken)
        {
            try
            {
                var report = await _repository.GetErrorReport(request.SiteCode);
                var errorReport = _mapper.Map<ErrorReportDto>(report);
                return Result.Ok(errorReport);
            }
            catch (Exception e)
            {
                Log.Error(e,"Report Error");
                   return Result.Fail<ErrorReportDto>(e.Message);
            }
        }
    }

}