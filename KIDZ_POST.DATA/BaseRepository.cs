

namespace KIDZ_POST.DATA
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using CONTRACT;

    public class BaseRepository<TEntity> : IRepository where TEntity : class, IEntity
    {
        private IStorageContext context;

        protected DbSet<TEntity> DbSet { get; private set; }
        

        //public int Save()
        //{
        //    return context.SaveChanges();
        //}

        public async Task<int> SaveAsync()
        {
            return await context.SaveAsync();
        }

        public void SetStorageContext(IStorageContext context)
        {
            this.context = context;
            this.DbSet = this.context.Set<TEntity>();
        }
    }
}
