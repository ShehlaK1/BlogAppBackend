using BlogAppBackend.Entities;
using Microsoft.EntityFrameworkCore;


namespace BlogAppBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        private IConfigurationRoot _configuration { get; set; }

        public ApplicationDbContext()
        {
            InitConfigurations();
        }

        private void InitConfigurations()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            _configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration != null)
            {
                string connectionString = _configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
        public virtual DbSet<Blog> Blogs { get; set; }

        public override int SaveChanges()
        {
            //UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken =
          default)
        {
            //UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        //private void UpdateTimestamps()
        //{
        //    var entities = ChangeTracker.Entries()
        //      .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        //    foreach (var entityEntry in entities)
        //    {
        //        var entity = (BaseEntity)entityEntry.Entity;

        //        if (entityEntry.State == EntityState.Added)
        //        {
        //            entity.CreatedAt = DateTime.UtcNow;
        //        }
        //        entity.CreatedBy = loggedInUserId;
        //        entity.UpdatedAt = DateTime.UtcNow;
        //    }
        //}

    }
}
