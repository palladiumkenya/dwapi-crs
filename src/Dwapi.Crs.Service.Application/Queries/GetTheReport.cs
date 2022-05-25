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
    public class GetTheReport:IRequest<Result<List<TheReportDto>>>
    {
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