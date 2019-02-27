namespace UnitOfWork.Sample.WebApi
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using UnitOfWork.Sample.Domain.Services;
    using UnitOfWork.Sample.Services.Implementation;
    using UnitOfWork.Sample.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using UnitOfWork.EFCore;
    using UnitOfWork.Sample.WebApi.DependencyResolution;
    using Swashbuckle.AspNetCore.Swagger;
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
            IoC.DependencyResolution.IoC.Iniailize(new DefaultRegistry());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TargetDatabase")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddUnitOfWorkForWorkers<IDataProvider, AppDbContext>();

            services.AddTransient<IArticleService, ArticleService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
