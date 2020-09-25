using System;
using System.Threading.Tasks;

namespace PersistenceAbstraction
{
    public interface IPersistence
    {
        Task InsertAsync<T>(T data) where T : class;

        Task UpdateAsync<T>(T data) where T : class;

        Task DeleteAsync<T>(T data) where T : class;
    }
}
