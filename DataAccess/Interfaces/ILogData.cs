using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ILogData<T>
    {
        public Task<string> InsertAsync(T Log);
    }
}
