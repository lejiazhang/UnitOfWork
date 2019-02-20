namespace UnitOfWork.Core
{
    using System;
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
