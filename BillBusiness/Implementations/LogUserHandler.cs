using BillBusiness.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Threading.Tasks;

namespace BillBusiness.Implementations
{
    public class LogUserHandler : ILog<Log>
    {
        private readonly ILogData<Log> _logData;
        public LogUserHandler(ILogData<Log> logData)
        {
            _logData = logData;
        }
        public async Task<string> InsertAsync(Log Log)
        {
            string result = await _logData.InsertAsync(Log);

            if (string.IsNullOrEmpty(result))
            {
                throw new Exception();
            }
            return result;
        }
    }
}
