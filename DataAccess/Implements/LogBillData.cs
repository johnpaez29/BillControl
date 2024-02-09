using DataAccess.Interfaces;
using DataAccess.Models;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace DataAccess.Implements
{
    public class LogBillData : BaseData, ILogData<LogBill>
    {
        public async Task<string> InsertAsync(LogBill Log)
        {
            var result = await FirebaseClient.Child("LogBill").PostAsync(Log);

            return result.Key;
        }
    }
}
