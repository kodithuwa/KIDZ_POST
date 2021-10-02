namespace KIDZ_POST.DATA.CONTRACT
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public interface IStorageContext
    {
        Task<int> SaveAsync();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
