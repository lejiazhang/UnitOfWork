namespace UnitOfWork.EFCore
{
    using global::UnitOfWork.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class UnitOfWorkConfiguration
    {
        public static void AddUnitOfWorkForWorkers<TTargetDbContextInterface, TTargetDbContext>(this IServiceCollection services)
            where TTargetDbContext : DbContext, TTargetDbContextInterface where TTargetDbContextInterface : class
        {
            services.AddScoped<TTargetDbContextInterface, TTargetDbContext>(container => container.GetService<TTargetDbContext>());
            services.AddScoped<IUnitOfWorkContext, UnitOfWorkContext<TTargetDbContext>>();
            services.AddScoped<UnitOfWork<TTargetDbContextInterface>>();
            services.AddScoped<IUnitOfWork>(container => container.GetService<UnitOfWork<TTargetDbContextInterface>>());
            services.AddScoped<IUnitOfWork<TTargetDbContextInterface>>(container => container.GetService<UnitOfWork<TTargetDbContextInterface>>());
        }
    }
}
