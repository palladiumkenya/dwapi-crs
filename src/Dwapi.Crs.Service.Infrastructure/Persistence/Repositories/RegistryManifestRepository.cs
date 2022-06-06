using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Dapper;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.Service.Application.Domain.Dtos;
using Dwapi.Crs.Service.Application.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.Crs.Service.Infrastructure.Repositories
{
    public class RegistryManifestRepository : IRegistryManifestRepository
    {
        private readonly CrsServiceContext _context;

        public RegistryManifestRepository(CrsServiceContext context)
        {
            _context = context;
        }

        public async Task<List<Manifest>> GetFirstTimers()
        {
            var sql = @"select * from vFirstTimers";
            return await _context.Manifests.FromSqlRaw(sql).ToListAsync();
        }

        public async Task<bool> Generate()
        {
            var list = new List<RegistryManifest>();

            try
            {
                // get first time
                var firstTimes = await GetFirstTimers();

                if (firstTimes.Any())
                {
                    foreach (var firstTime in firstTimes)
                    {
                        var man = RegistryManifest.Create(firstTime);
                        list.Add(man);
                    }
                }

                if (list.Any())
                {
                   _context.AddRange(list);
                   await _context.SaveChangesAsync();
                } 

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e,"Generate error");
                throw;
            }
        }

        public async Task<bool> Process()
        {
            try
            {
                var manis = _context.RegistryManifests
                    .Where(x => !x.Records.HasValue)
                    .ToList();
               
                foreach (var mani in manis)
                {
                    var count = _context.ClientRegistries.LongCount(x => x.FacilityId == mani.FacilityId);
                    mani.UpdateRecords(count);
                    var activeCount = _context.ClientRegistries.LongCount(x => x.FacilityId == mani.FacilityId && (x.CurrentOnART.ToLower()=="yes" || x.CurrentOnART.ToLower()=="y"));
                    mani.UpdateActiveRecords(activeCount);
                    _context.Update(mani);
                 await   _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e,"Generate error");
                throw;
            }
        }


        public Task<List<RegistryManifest>> GetReadyForSending(int [] siteCode=null)
        {
            if (null != siteCode)
            {
                var list = _context.RegistryManifests.ToList()
                    .Where(x => x.CanBeSent && siteCode.Contains(x.SiteCode))
                    .ToList();
                return Task.FromResult(list);
            }

            var ls = _context.RegistryManifests.ToList()
                .Where(x => x.CanBeSent)
                .ToList();
                
            return Task.FromResult(ls);
        }

        public async Task<RegistryManifest> GetErrorReport(int siteCode)
        {
            var site = _context.RegistryManifests.AsNoTracking().FirstOrDefault(x => x.SiteCode == siteCode);
            if (null != site)
                site.TransmissionLogs = _context.TransmissionLogs.AsNoTracking()
                    .Where(x => x.RegistryManifestId == site.Id && x.Response == Response.Failed).ToList();

            return site;
        }
        
        public Task<List<TheReportDto>> GetTheReport()
        {
            var sql = @"select * from vTheReport order by DateArrived desc,SiteCode,ResponseStatusDate desc";
            var ls = _context.Database.GetDbConnection().Query<TheReportDto>(sql).ToList();
            return Task.FromResult(ls);
        }
    }
}
