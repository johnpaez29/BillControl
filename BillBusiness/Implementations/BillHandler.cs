using BillBusiness.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillBusiness.Handlers
{
    public class BillHandler : IBillHandler
    {
        private readonly IServiceData<BillData> _serviceData;
        private readonly IServiceBillData<BillData> _serviceBillData;

        public BillHandler(
            IServiceData<BillData> serviceData,
            IServiceBillData<BillData> serviceBillData)
        {
            _serviceData = serviceData;
            _serviceBillData = serviceBillData;
        }
        public async Task<IEnumerable<BillData>> GetBills()
        {
            IEnumerable<BillData> bills = await _serviceData.GetAllAsync();
            return bills;
        }

        public async Task<string> InsertBill(BillControl bill)
        {
            IEnumerable<BillData> billData = GetBill(bill);

            string id = Guid.NewGuid().ToString();
            foreach (BillData data in billData)
            {
                data.IdBill = id;
                await _serviceData.InsertOne(data);
            }

            return id;
        }

        private IEnumerable<BillData> GetBill(BillControl bill)
        {
            DateTime actualDate = DateTime.UtcNow.AddHours(-5);
            DateTime lastDate = GetDate(actualDate.Year, actualDate.Month, bill.PayDay);

            List<BillData> billData = new List<BillData>();
            bool firstMonth = true;
            foreach (int month in Bill.months.Where(month => month >= actualDate.Month))
            {
                BillData billMonth = new BillData
                {
                    IdUser = bill.IdUser,
                    PayDate = GetDate(actualDate.Year, month, bill.PayDay),
                    Name = bill.Name,
                    State = (firstMonth && actualDate > lastDate) ? States.ATRASADO.ToString() : States.PENDIENTE.ToString(),
                };

                billData.Add(billMonth);

                firstMonth = false;
            }

            return billData;
        }

        private DateTime GetDate(int year, int month, int day)
        {
            for (int indexDay = day; indexDay > 0; indexDay--)
            {
                try
                {
                    return new DateTime(year, month, indexDay);
                }
                catch 
                {
                    continue;
                }
            }

            return DateTime.UtcNow.AddHours(-5);
        }

        public async Task<IEnumerable<BillData>> GetBillsByIdUser(string id)
        {
            IEnumerable<BillData> bills = await _serviceBillData.GetAllByIdUserAsync(id);
            return bills;
        }

        public async Task DeleteBill(string id)
        {
            IEnumerable<BillData> bills = await _serviceBillData.GetAllByIdBillAsync(id);

            foreach (BillData bill in bills)
            {
                await _serviceData.Delete(bill.Id);
            }
        }

        public async Task UpdateBill(UpdateBill bill)
        {
            if (bill.IsPartial)
            {
                await _serviceData.Update(bill.Bill);
            }
            else
            {
                var bills = await GetBillsByIdUser(bill.Bill.IdUser);
                bills = bills.Where(bill => bill.PayDate.Month <= DateTime.UtcNow.AddHours(-5).Month);

                foreach(var billDate in bills)
                {
                    billDate.State = bill.Bill.State;
                    await _serviceData.Update(billDate);
                }
            }
        }
    }
}
