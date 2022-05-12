using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.SharedKernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dwapi.Crs.Service.Infrastructure
{
    public class CrsServiceContext : BaseContext
    {
        public DbSet<RegistryManifest> RegistryManifests { get; set; }
        public DbSet<TransmissionLog> TransmissionLogs { get; set; }
        
        public CrsServiceContext(DbContextOptions<CrsServiceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         //   DapperPlusManager.Entity<Docket>().Key(x => x.Id).Table($"{nameof(CrsContext.Dockets)}");
         
        }

    }
}
