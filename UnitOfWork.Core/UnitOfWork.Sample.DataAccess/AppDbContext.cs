namespace UnitOfWork.Sample.DataAccess
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Configuration;
    using Microsoft.Extensions.Configuration;
    using UnitOfWork.Sample.DataAccess.Entities;
    using UnitOfWork.Sample.DataAccess.EntitiesConfiguration;
    using UnitOfWork.Sample.DataAccess.Extensions;

    public class AppDbContext : DbContext, IDataProvider
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddConfiguration(new ArticleConfig());
        }
    }
}
