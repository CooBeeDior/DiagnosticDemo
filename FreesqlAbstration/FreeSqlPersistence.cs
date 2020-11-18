using System.Threading.Tasks;
namespace FreesqlAbstration
{
    public class FreeSqlTransPortService : IFreeSqlTransPortService
    {
        private readonly IFreeSql _freeSql;
        public FreeSqlTransPortService(IdleBus<IFreeSql> idleBus)
        {
            _freeSql = idleBus.Get("FreeSqlDb");
        }
        public async Task DeleteAsync<T>(T data) where T : class
        {
            await _freeSql.Delete<T>(data).ExecuteAffrowsAsync();
        }

        public async Task Send<T>(T data) where T : class
        {
            await _freeSql.Insert(data).ExecuteAffrowsAsync();

        }

        public async Task UpdateAsync<T>(T data) where T : class
        {
            await _freeSql.Update<T>(data).ExecuteAffrowsAsync();
        }
    }
}
