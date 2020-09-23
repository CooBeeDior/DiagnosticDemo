using System;

namespace PersistenceAbstraction
{
    public interface IPersistence
    {
        void Insert<T>(T data) where T : class;

        void Update<T>(T data) where T : class;

        void Delete<T>(T data) where T : class;
    }
}
