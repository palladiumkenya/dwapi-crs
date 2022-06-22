using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Service.Application.Domain.Dtos;
using Dwapi.Crs.Service.Application.Interfaces;
using System.Data.SqlClient;
using Dwapi.Crs.SharedKernel.Custom;
using Dwapi.Crs.SharedKernel.Enums;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.Crs.Service.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly CrsServiceContext _context;

        public ClientRepository(CrsServiceContext context)
        {
            _context = context;
        }

        public List<ClientRegistry> Load(int page, int pageSize, Guid facilityId)
        {
            page = page < 0 ? 1 : page;
            pageSize = pageSize < 0 ? 1 : pageSize;
            
             
            var skip = (page - 1) * pageSize; 
            
            return _context
                .ClientRegistries
                .Where(x => x.FacilityId == facilityId)
                .OrderBy(x=>x.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }

        public List<SiteDuplicateDto> LoadDuplicateSummary()
        {
            var sqlD = @"
                            select f.Id,f.SiteCode,f.Name,s.NoOfDuplicates,s.PatientPk from  Facilities f inner join 
                            (select count(ID) NoOfDuplicates,SiteCode,PatientPk from ClientRegistries
                            group by SiteCode, PatientPk
                            HAVING (COUNT(Id) > 1)) s on f.SiteCode=s.SiteCode";

            var sitesDuplicates= _context.Database.GetDbConnection()
                .Query<SiteDuplicateSummaryDto>(sqlD)
                .ToList();

            return SiteDuplicateDto.New(sitesDuplicates);
        }

        public List<ClientRegistry> LoadDuplicates(SiteDuplicateDto siteDuplicate)
        {
            var sql = @$"
                        select * from ClientRegistries 
                        where 
                            SiteCode={siteDuplicate.SiteCode} and 
                            PatientPk in ({siteDuplicate.AllPatientPks})
                        ";
            var clients = _context.ClientRegistries.FromSqlRaw(sql);
            return clients.ToList();
        }

        public Task<int> DeDuplicate(SiteDuplicateDto siteDuplicate,IProgress<AppProgress> progress = null)
        {
            
            var appProgress = AppProgress.New(Area.Deduplicating,$"Deduplicating {siteDuplicate.Name}...", 0);
            if(null!=progress)
                progress.Report(appProgress);
            
            var duplicates = LoadDuplicates(siteDuplicate);
            
            if (!duplicates.Any())
                return Task.FromResult(0);
            
            var clientGroups = duplicates.GroupBy(x => new { x.SiteCode, x.PatientPk });
           
            var deduplicatedClients = new List<ClientRegistry>();
         
            foreach (var grouping in clientGroups)
            {
                deduplicatedClients.AddRange(grouping.Take(1));
            }

            var clientsToDelete = duplicates.Except(deduplicatedClients).ToList();
   
            using( System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(_context.Database.GetDbConnection().ConnectionString)) {
                cn.Open();
                using(var tran = cn.BeginTransaction()) {
                    try {
                        tran.BulkDelete(clientsToDelete);
                        tran.Commit();
                    } catch (Exception ex) {
                        tran.Rollback();
                        Log.Error(ex, "Delete error!");
                    }
                }
            }
            
            appProgress.UpdateDone($"Deduplicating {siteDuplicate.Name}... Done!");
            if(null!=progress)
                progress.Report(appProgress);
            
            Log.Debug(appProgress.Report);
            
            return Task.FromResult(clientsToDelete.Count());
        }
        
    
    }



}
