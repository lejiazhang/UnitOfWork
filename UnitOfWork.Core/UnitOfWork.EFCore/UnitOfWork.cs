namespace UnitOfWork.EFCore
{
    using UnitOfWork.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Threading.Tasks;

    public class UnitOfWork<T> : IUnitOfWork<T>
    {

        private readonly IUnitOfWorkContext _context;
        private ITransaction _transaction;
        private IsolationLevel? _isolationLevel;

        public T DataProvider { get; }

        public bool HasOpenTransaction => _transaction != null;

        public UnitOfWork(IUnitOfWorkContext context, T dataProvider)
        {
            _context = context;
            DataProvider = dataProvider;
        }

        private void StartNewTransactionIfNeeded()
        {
            if(_transaction == null)
            {
                if (_isolationLevel.HasValue)
                    _transaction = _context.BeginTransaction(_isolationLevel.GetValueOrDefault());
                else
                    _transaction = _context.BeginTransaction();
            }
        }

        public void BeginTransactionManually()
        {
            StartNewTransactionIfNeeded();
        }

        public void CommitTransaction()
        {
            _context.SaveChanges();
            if(_transaction != null)
            {
                _transaction.Commit();

                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void CommitTransaction(IsolationLevel isolationLevel)
        {
            CommitTransaction();
            _isolationLevel = isolationLevel;
        }

        public void RollbackTransaction()
        {
            if (_transaction == null) return;

            _transaction.Rollback();

            _transaction.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction(IsolationLevel isolationLevel)
        {
            RollbackTransaction();

            _isolationLevel = isolationLevel;
        }

        public int SaveChanges()
        {
            StartNewTransactionIfNeeded();

            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            StartNewTransactionIfNeeded();

            return await _context.SaveChangesAsync();
        }

        public void SetIsolationLevel(IsolationLevel isolationLevel)
        {
            _isolationLevel = isolationLevel;
        }
        public void Dispose()
        {
            if (_transaction != null) _transaction.Dispose();
            _transaction = null;
        }
    }
}
