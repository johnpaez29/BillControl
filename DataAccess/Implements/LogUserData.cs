using DataAccess.Interfaces;
using DataAccess.Models;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace DataAccess.Implements
{
    public class LogUserData : BaseData, ILogData<Log>
    {
        public async Task<string> InsertAsync(Log Log)
        {
            var result = await FirebaseClient.Child("LogUser").PostAsync(Log);

            return result.Key;
        }
    }
}
