using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.Service.Application.Domain;
using Dwapi.Crs.SharedKernel.Infrastructure.Data;
using Dwapi.Crs.SharedKernel.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.Crs.Service.Infrastructure
{
    public class CrsServiceContext : BaseContext
    {
        public DbSet<RegistryManifest> RegistryManifests { get; set; }
        public DbSet<TransmissionLog> TransmissionLogs { get; set; }
        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<ClientRegistry> ClientRegistries { get; set; }
        
        public DbSet<SpotFacility> SpotFacilities { get; set; }
        
        public CrsServiceContext(DbContextOptions<CrsServiceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore<Manifest>();
            //modelBuilder.Ignore<ClientRegistry>(); 
            
            modelBuilder.Entity<Manifest>(entity => entity.ToView("Manifests", "dbo"));
            modelBuilder.Entity<ClientRegistry>(entity => entity.ToView("ClientRegistries", "dbo")); 
            
            DapperPlusManager.Entity<RegistryManifest>().Key(x => x.Id).Table($"{nameof(RegistryManifests)}");
            DapperPlusManager.Entity<TransmissionLog>().Key(x => x.Id).Table($"{nameof(TransmissionLogs)}");
        }

        public override void EnsureSeeded()
        {
            Log.Debug("seeding...");
            if (!SpotFacilities.Any())
            {
                var data = SeedDataReader.ReadCsv<SpotFacility>(typeof(CrsServiceContext).Assembly,"Seed","|");
                SpotFacilities.AddRange(data);
            }
            SaveChanges();
            Log.Debug("seeding DONE");
        }
    }
}
