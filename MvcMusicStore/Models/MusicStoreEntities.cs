using System.Data.Entity;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MvcMusicStore.Models
{
    public class MusicStoreEntitiesPostgreSqlConfiguration : DbConfiguration
    {
        public MusicStoreEntitiesPostgreSqlConfiguration()
        {
            SetProviderServices("Npgsql", Npgsql.NpgsqlServices.Instance);
            SetDefaultConnectionFactory(new Npgsql.NpgsqlConnectionFactory());
        }
    }

    [DbConfigurationType(typeof(MusicStoreEntitiesPostgreSqlConfiguration))]
    public class MusicStoreEntities : DbContext
    {
        public MusicStoreEntities() : base(GetConnectionString())
        {
            // Database.SetInitializer removed - not applicable for PostgreSQL
        }

        private static string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            
            return config.GetConnectionString("MusicStoreEntities") ?? "Host=localhost;Database=postgres;Username=postgres;Password=postgres;";
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Apply schema mappings for PostgreSQL target schema: mvcmusicentities_dbo
            
            // Albums table mapping
            modelBuilder.Entity<Album>()
                .ToTable("albums", "mvcmusicentities_dbo")
                .Property(e => e.AlbumId).HasColumnName("albumid");
            modelBuilder.Entity<Album>()
                .Property(e => e.GenreId).HasColumnName("genreid");
            modelBuilder.Entity<Album>()
                .Property(e => e.ArtistId).HasColumnName("artistid");
            modelBuilder.Entity<Album>()
                .Property(e => e.Title).HasColumnName("title");
            modelBuilder.Entity<Album>()
                .Property(e => e.Price).HasColumnName("price");
            modelBuilder.Entity<Album>()
                .Property(e => e.AlbumArtUrl).HasColumnName("albumarturl");

            // Genres table mapping
            modelBuilder.Entity<Genre>()
                .ToTable("genres", "mvcmusicentities_dbo")
                .Property(e => e.GenreId).HasColumnName("genreid");
            modelBuilder.Entity<Genre>()
                .Property(e => e.Name).HasColumnName("name");
            modelBuilder.Entity<Genre>()
                .Property(e => e.Description).HasColumnName("description");

            // Artists table mapping
            modelBuilder.Entity<Artist>()
                .ToTable("artists", "mvcmusicentities_dbo")
                .Property(e => e.ArtistId).HasColumnName("artistid");
            modelBuilder.Entity<Artist>()
                .Property(e => e.Name).HasColumnName("name");

            // Carts table mapping
            modelBuilder.Entity<Cart>()
                .ToTable("carts", "mvcmusicentities_dbo")
                .Property(e => e.RecordId).HasColumnName("recordid");
            modelBuilder.Entity<Cart>()
                .Property(e => e.CartId).HasColumnName("cartid");
            modelBuilder.Entity<Cart>()
                .Property(e => e.AlbumId).HasColumnName("albumid");
            modelBuilder.Entity<Cart>()
                .Property(e => e.Count).HasColumnName("count");
            modelBuilder.Entity<Cart>()
                .Property(e => e.DateCreated).HasColumnName("datecreated");

            // Orders table mapping
            modelBuilder.Entity<Order>()
                .ToTable("orders", "mvcmusicentities_dbo")
                .Property(e => e.OrderId).HasColumnName("orderid");
            modelBuilder.Entity<Order>()
                .Property(e => e.OrderDate).HasColumnName("orderdate");
            modelBuilder.Entity<Order>()
                .Property(e => e.Username).HasColumnName("username");
            modelBuilder.Entity<Order>()
                .Property(e => e.FirstName).HasColumnName("firstname");
            modelBuilder.Entity<Order>()
                .Property(e => e.LastName).HasColumnName("lastname");
            modelBuilder.Entity<Order>()
                .Property(e => e.Address).HasColumnName("address");
            modelBuilder.Entity<Order>()
                .Property(e => e.City).HasColumnName("city");
            modelBuilder.Entity<Order>()
                .Property(e => e.State).HasColumnName("state");
            modelBuilder.Entity<Order>()
                .Property(e => e.PostalCode).HasColumnName("postalcode");
            modelBuilder.Entity<Order>()
                .Property(e => e.Country).HasColumnName("country");
            modelBuilder.Entity<Order>()
                .Property(e => e.Phone).HasColumnName("phone");
            modelBuilder.Entity<Order>()
                .Property(e => e.Email).HasColumnName("email");
            modelBuilder.Entity<Order>()
                .Property(e => e.Total).HasColumnName("total");

            // OrderDetails table mapping
            modelBuilder.Entity<OrderDetail>()
                .ToTable("orderdetails", "mvcmusicentities_dbo")
                .Property(e => e.OrderDetailId).HasColumnName("orderdetailid");
            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.OrderId).HasColumnName("orderid");
            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.AlbumId).HasColumnName("albumid");
            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Quantity).HasColumnName("quantity");
            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.UnitPrice).HasColumnName("unitprice");

            base.OnModelCreating(modelBuilder);
        }
    }
}