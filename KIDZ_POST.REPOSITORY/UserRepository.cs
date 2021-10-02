namespace KIDZ_POST.Repository
{
    using KIDZ.POST.REPOSITORY.CONTRACT;
    using KIDZ_POST.DATA;
    using KIDZ_POST.DATA.CONTRACT;
    using KIDZ_POST.DATA.MODEL;
    using System.Linq;
    using System.Threading.Tasks;


    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IStorageContext context;

        public UserRepository(IStorageContext context)
        {
            this.context = context;
        }

        public IQueryable<User> Get()
        {

            var products = this.DbSet.Select(x => new User
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                Password = x.Password,
                Description = x.Description,
                IsActivated = x.IsActivated,
            });

            return products;
        }

        public async Task<User> Create(User user)
        {
            var entity = new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
                Description = user.Description,
                IsActivated = user.IsActivated,
            };
            var obj = DbSet.Add(entity).Entity;
            await this.SaveAsync();
            return obj;
        }
    }
}
