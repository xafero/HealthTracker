using NHibernate;
using System;

namespace HealthTracker.Data
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        public void Delete(T instance)
        {
            using (ISession session = Database.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(instance);
                transaction.Commit();
            }
        }

        public T Get(Guid id)
        {
            using (ISession session = Database.OpenSession())
                return session.Get<T>(id);
        }

        public long RowCount()
        {
            using (ISession session = Database.OpenSession())
                return session.QueryOver<T>().RowCountInt64();
        }

        public void Save(T instance)
        {
            using (ISession session = Database.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(instance);
                transaction.Commit();
            }
        }

        public void Update(T instance)
        {
            using (ISession session = Database.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(instance);
                transaction.Commit();
            }
        }
    }
}