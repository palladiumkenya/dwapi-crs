using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Service.Application.Domain;
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
            var sql = @"
                            select * from (SELECT 
                                     *,RANK() OVER (PARTITION BY SiteCode ORDER BY DateArrived ASC ) rank_no
                                FROM Manifests WHERE [End] is not null)x
                            where rank_no=1 and SiteCode not in (select SiteCode from RegistryManifests)
                      ";

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
                Log.Error("Generate error", e);
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
                    _context.Update(mani);
                 await   _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Generate error", e);
                throw;
            }
        }


        public Task<List<RegistryManifest>> GetReadyForSending()
        {
            var list= _context.RegistryManifests.ToList()
                .Where(x => x.CanBeSent)
                .ToList();
            
            return Task.FromResult(list);
        }
    }
}
