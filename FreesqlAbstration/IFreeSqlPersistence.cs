using PersistenceAbstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreesqlAbstration
{
    public interface IFreeSqlPersistence : IPersistence
    {
    }

    public class FreeSqlPersistence : IFreeSqlPersistence
    {
        private readonly IFreeSql _freeSql;
        public FreeSqlPersistence(Func<string, IFreeSql> func)
        {
            _freeSql = func.Invoke("FreeSql");
        }
        public Task DeleteAsync<T>(T data) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync<T>(T data) where T : class
        {
            await _freeSql.Insert(data).ExecuteAffrowsAsync();

        }

        public Task UpdateAsync<T>(T data) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
