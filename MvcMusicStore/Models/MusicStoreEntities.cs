using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
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
            SetDefaultConnectionFactory(new Npgsql.NpgsqlConnectionFactory());
            SetProviderFactory("Npgsql", NpgsqlFactory.Instance);
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

            return config.GetConnectionString("MusicStoreEntities") ?? "Host=localhost;Database=atx-tgt-172-31-93-10;Username=postgres;Password=postgres;";
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}