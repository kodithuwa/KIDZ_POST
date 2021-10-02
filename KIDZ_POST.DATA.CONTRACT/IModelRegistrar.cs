

namespace KIDZ_POST.DATA.CONTRACT
{
    using Microsoft.EntityFrameworkCore;

    public interface IModelRegistrar
    {
        void RegisterModels(ModelBuilder modelBuilder);
    }
}
