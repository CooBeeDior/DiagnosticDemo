using PersistenceAbstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace FreesqlAbstration
{
    public class FreeSqlPersistence : IFreeSqlPersistence
    {
        private readonly IFreeSql _freeSql;
        public FreeSqlPersistence(IdleBus<IFreeSql> idleBus)
        {
            _freeSql = idleBus.Get("FreeSqlDb");
        }
        public async Task DeleteAsync<T>(T data) where T : class
        {
            await _freeSql.Delete<T>(data).ExecuteAffrowsAsync();
        }

        public async Task InsertAsync<T>(T data) where T : class
        {
            await _freeSql.Insert(data).ExecuteAffrowsAsync();

        }

        public async Task UpdateAsync<T>(T data) where T : class
        {
            await _freeSql.Update<T>(data).ExecuteAffrowsAsync();
        }
    }
}
