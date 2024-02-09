using System.Threading.Tasks;

namespace BillBusiness.Interfaces
{
    public interface ILog<T>
    {
        public Task<string> InsertAsync(T Log);

    }
}
