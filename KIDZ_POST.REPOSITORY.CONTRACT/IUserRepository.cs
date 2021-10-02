

namespace KIDZ.POST.REPOSITORY.CONTRACT
{
    using KIDZ_POST.DATA.CONTRACT;
    using KIDZ_POST.DATA.MODEL;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IUserRepository : IRepository
    {
        IQueryable<User> Get();

        Task<User> Create(User user);

    }
}
