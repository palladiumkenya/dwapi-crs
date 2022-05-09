using Dwapi.Crs.Core.Domain;
using Dwapi.Crs.SharedKernel.Infrastructure.Data;
using Dwapi.Crs.SharedKernel.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Serilog;
using Z.Dapper.Plus;

namespace Dwapi.Crs.Infrastructure.Data
{
    public class CrsContext : BaseContext
    {
        public DbSet<Docket> Dockets { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<MasterFacility> MasterFacilities { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<Cargo> Cargoes { get; set; }
        public DbSet<ClientRegistry> ClientRegistries { get; set; }
        

        public CrsContext(DbContextOptions<CrsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DapperPlusManager.Entity<Docket>().Key(x => x.Id).Table($"{nameof(CrsContext.Dockets)}");
            DapperPlusManager.Entity<Subscriber>().Key(x => x.Id).Table($"{nameof(CrsContext.Subscribers)}");
            DapperPlusManager.Entity<MasterFacility>().Key(x => x.Id).Table($"{nameof(CrsContext.MasterFacilities)}");
            DapperPlusManager.Entity<Facility>().Key(x => x.Id).Table($"{nameof(CrsContext.Facilities)}");
            DapperPlusManager.Entity<Manifest>().Key(x => x.Id).Table($"{nameof(CrsContext.Manifests)}");
            DapperPlusManager.Entity<Cargo>().Key(x => x.Id).Table($"{nameof(CrsContext.Cargoes)}");
            DapperPlusManager.Entity<ClientRegistry>().Key(x => x.Id).Table($"{nameof(CrsContext.ClientRegistries)}");
        }

        public override void EnsureSeeded()
        {
            Log.Debug("seeding...");
            /*
            if (!MasterFacilities.Any())
            {
                var data = SeedDataReader.ReadCsv<MasterFacility>(typeof(CrsContext).Assembly,"Seed","|");
                MasterFacilities.AddRange(data);
            }
            */

            if (!Dockets.Any())
            {
                var data = SeedDataReader.ReadCsv<Docket>(typeof(CrsContext).Assembly,"Seed","|");
                Dockets.AddRange(data);
            }

            if (!Subscribers.Any())
            {
                var data = SeedDataReader.ReadCsv<Subscriber>(typeof(CrsContext).Assembly,"Seed","|");
                Subscribers.AddRange(data);
            }
            SaveChanges();
            Log.Debug("seeding DONE");
        }
    }
}
