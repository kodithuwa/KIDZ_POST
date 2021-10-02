namespace KIDZ_POST.DATA
{
    using KIDZ_POST.DATA.CONTRACT;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class StorageContext :DbContext , IStorageContext
    {
        public StorageContext(DbContextOptions<StorageContext> options)
            : base(options)
        {
        }

        //public DbSet<TEntity> DbSet<TEntity>() where TEntity : class, IEntity
        //{
        //    return this.DbSet<TEntity>();
        //}

        public async Task<int> SaveAsync()
        {
            return await base.SaveChangesAsync();
        }

        //public void Migrate()
        //{
        //    this.Database.mi();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new ModelRegistrar().RegisterModels(builder);
        }
    }
}

