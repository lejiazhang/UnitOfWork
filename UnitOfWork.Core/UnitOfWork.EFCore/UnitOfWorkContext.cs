namespace UnitOfWork.EFCore
{
    using global::UnitOfWork.Core;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Threading.Tasks;

    public class UnitOfWorkContext<T> : IUnitOfWorkContext where T : DbContext
    {
        private readonly T _dbContext;
        public UnitOfWorkContext(T dbContext)
        {
            _dbContext = dbContext;
        }

        public ITransaction BeginTransaction()
        {
            var dbTransaction = _dbContext.Database.BeginTransaction();

            return new Transaction(dbTransaction);
        }

        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            var dbTransaction = _dbContext.Database.BeginTransaction(isolationLevel);

            return new Transaction(dbTransaction);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
