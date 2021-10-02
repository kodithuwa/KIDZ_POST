
namespace KIDZ_POST.DATA
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using CONTRACT;
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IStorageContext context;
        private readonly IServiceProvider serviceProvider;

        public RepositoryWrapper(IStorageContext context, IServiceProvider serviceProvider)
        {
            this.context = context;
            this.serviceProvider = serviceProvider;
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            var repostory = this.serviceProvider.GetService<TRepository>();
            repostory.SetStorageContext(this.context);
            return repostory;
        }
    }

}
