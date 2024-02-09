using BillBusiness.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Threading.Tasks;

namespace BillBusiness.Implementations
{
    public class LogBillHandler : ILog<LogBill>
    {
        private readonly ILogData<LogBill> _logData;
        public LogBillHandler(ILogData<LogBill> logData)
        {
            _logData = logData;
        }
        public async Task<string> InsertAsync(LogBill Log)
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
