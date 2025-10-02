using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.Extensions.Configuration;
using System.IO;
using Npgsql;

namespace MvcMusicStore.Models
{
    public class MusicStoreEntitiesPostgreSqlConfiguration : DbConfiguration
    {
        public MusicStoreEntitiesPostgreSqlConfiguration()
        {
            SetProviderServices("Npgsql", Npgsql.NpgsqlServices.Instance);
            SetDefaultConnectionFactory(new NpgsqlConnectionFactory());
        }
    }

    [DbConfigurationType(typeof(MusicStoreEntitiesPostgreSqlConfiguration))]
    public class MusicStoreEntities : DbContext
    {
        public MusicStoreEntities() : base(GetConnectionString())
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MusicStoreEntities>());
        }

        private static string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            return config.GetConnectionString("MusicStoreEntities") ?? "Data Source=MvcMusicStore.db";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("mvcmusicentities_dbo");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}