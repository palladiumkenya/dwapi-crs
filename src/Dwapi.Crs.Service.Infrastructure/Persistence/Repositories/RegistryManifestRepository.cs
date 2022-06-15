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
using Dwapi.Crs.SharedKernel.Custom;
using Dwapi.Crs.SharedKernel.Enums;
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

        public async Task<int> Generate(IProgress<AppProgress> progress=null)
        {
            var list = new List<RegistryManifest>();
            var appProgress = AppProgress.New(  Area.Generating,"Generating List...", 0);
            if(null!=progress)
                progress.Report(appProgress);

            try
            {
                // get first time
                var firstTimes = await GetFirstTimers();

                if (firstTimes.Any())
                {
                    int i = 0;
                    foreach (var firstTime in firstTimes)
                    {
                        i++;
                        appProgress.Update($"Generating List... {i} of {firstTimes.Count}",i,firstTimes.Count);
                        var man = RegistryManifest.Create(firstTime);
                        list.Add(man);
                    }
                }

               
                if (list.Any())
                {
                   _context.AddRange(list);
                   await _context.SaveChangesAsync();
                } 
                
                appProgress.UpdateDone($"Generating List of {list.Count} Done!");
                if(null!=progress)
                    progress.Report(appProgress);
                
                Log.Debug(appProgress.Report);
                
                return list.Count;
            }
            catch (Exception e)
            {
                Log.Error(e,"Generate error");
                throw;
            }
        }

        public async Task<int> Process(IProgress<AppProgress> progress=null)
        {
            
            var appProgress = AppProgress.New(Area.Processing,"Processing List...", 0);
            if(null!=progress)
                progress.Report(appProgress);

            try
            {

                var manis = _context.RegistryManifests
                    .Where(x => !x.Records.HasValue)
                    .ToList();
                int i = 0;
                foreach (var mani in manis)
                {
                    var count = _context.ClientRegistries.LongCount(x => x.FacilityId == mani.FacilityId);
                    mani.UpdateRecords(count);
                    var activeCount = _context.ClientRegistries.LongCount(x =>
                        x.FacilityId == mani.FacilityId &&
                        (x.CurrentOnART.ToLower() == "yes" || x.CurrentOnART.ToLower() == "y"));
                    mani.UpdateActiveRecords(activeCount);
                    _context.Update(mani);
                    await _context.SaveChangesAsync();
                    i++;
                    
                    appProgress.Update($"Processing List... {i}/{manis.Count}",i,manis.Count);
                    if(null!=progress)
                        progress.Report(appProgress);
                   
                    Log.Debug(appProgress.Report);
                }
                
                
                appProgress.Update($"Processing List {i}/{manis.Count} Done!",i,manis.Count);
                if(null!=progress)
                    progress.Report(appProgress);
                
                Log.Debug(appProgress.Report);

                return i;
            }
            catch (Exception e)
            {
                Log.Error(e, "Generate error");
                throw;
            }
        }

        public Task<List<RegistryManifest>> GetReadyForSending(bool newOnly=true,int [] siteCode=null)
        {
            if (newOnly)
                return GetNewReadyForSending(siteCode);
                
            if (null != siteCode)
            {
                var list = _context.RegistryManifests.AsNoTracking().ToList()
                    .Where(x => x.CanBeSent && siteCode.Contains(x.SiteCode))
                    .ToList();
                return Task.FromResult(list);
            }

            var ls = _context.RegistryManifests.AsNoTracking().ToList()
                .Where(x => x.CanBeSent)
                .ToList();
                
            return Task.FromResult(ls);
        }

        public Task<List<RegistryManifest>> GetNewForSending(int[] siteCode = null)
        {
            if (null != siteCode)
            {
                var list = _context.RegistryManifests.AsNoTracking().ToList()
                    .Where(x => x.CanBeSentNewOnly && siteCode.Contains(x.SiteCode))
                    .ToList();
                return Task.FromResult(list);
            }

            var ls = _context.RegistryManifests.AsNoTracking().ToList()
                .Where(x => x.CanBeSentNewOnly)
                .ToList();
                
            return Task.FromResult(ls);
        }

        public Task<List<RegistryManifest>> GetFailedForSending(int[] siteCode = null)
        {
            if (null != siteCode)
            {
                var list = _context.RegistryManifests.AsNoTracking().ToList()
                    .Where(x => x.CanBeSentFailed && siteCode.Contains(x.SiteCode))
                    .ToList();
                return Task.FromResult(list);
            }

            var ls = _context.RegistryManifests.AsNoTracking().ToList()
                .Where(x => x.CanBeSentFailed)
                .ToList();
                
            return Task.FromResult(ls);
        }

        private Task<List<RegistryManifest>> GetNewReadyForSending(int [] siteCode=null)
        {
    
             var all = _context.RegistryManifests.AsNoTracking()
                    .Include(c => c.TransmissionLogs);
                
            if (null != siteCode)
            {
                var list = all.ToList()
                    .Where(x => x.CanBeSent && siteCode.Contains(x.SiteCode))
                    .ToList();
                return Task.FromResult(list);
            }

            var ls = all.ToList()
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
