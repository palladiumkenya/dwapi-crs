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
    }
}
