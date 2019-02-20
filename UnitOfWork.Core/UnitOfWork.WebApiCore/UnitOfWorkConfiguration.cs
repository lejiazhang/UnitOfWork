namespace UnitOfWork.WebApiCore
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.EFCore;
    using Microsoft.AspNetCore.Mvc.Filters;
    using UnitOfWork.WebApiCore.Filters;

    public static class UnitOfWorkConfiguration
    {
        public static void AddUnitOfWorkForWebApi<TTargetDbContextInterface, TTargetDbContext>(this IServiceCollection services)
            where TTargetDbContext : DbContext, TTargetDbContextInterface where TTargetDbContextInterface : class
        {
            services.AddUnitOfWorkForWorkers<TTargetDbContextInterface, TTargetDbContext>();
            services.AddScoped<TransactionFilterAttribute>();
        }

        public static void AddUnitOfWorkTransactionAttribute(this FilterCollection filters)
        {
            filters.Add<TransactionFilterAttribute>();
        }
    }
}
