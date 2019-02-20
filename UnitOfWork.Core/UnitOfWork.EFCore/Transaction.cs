namespace UnitOfWork.EFCore
{
    using System;
    using UnitOfWork.Core;
    using Microsoft.EntityFrameworkCore.Storage;
    public class Transaction : ITransaction
    {
        private readonly IDbContextTransaction _transaction;
        public Transaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }
        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
