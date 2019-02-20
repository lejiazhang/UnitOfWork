namespace UnitOfWork.Core
{
    using System.Data;
    using System.Threading.Tasks;

    public interface IUnitOfWorkContext
    {
        ITransaction BeginTransaction();

        ITransaction BeginTransaction(IsolationLevel isolationLevel);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
