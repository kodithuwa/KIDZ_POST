namespace KIDZ_POST.DATA.CONTRACT
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public interface IRepositoryWrapper
    {
        TRepository GetRepository<TRepository>() where TRepository : IRepository;
    }
}
