using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBusiness.Handlers
{
    public class BillHandler : IBillHandler
    {
        private readonly IServiceData<BillControl> _serviceData;

        public BillHandler (IServiceData<BillControl> serviceData)
        {
            _serviceData = serviceData;
        }
        public async Task<IEnumerable<BillControl>> GetBills()
        {
            IEnumerable<BillControl> bills = await _serviceData.GetAllAsync();
            return bills.Select(bill => new BillControl
            {
                Id = bill.Id,
                Name = bill.Name,
                PayDay = bill.PayDay,
                State = GetState(bill),
                Months = bill.Months
            });
        }

        public void InsertBill(BillControl bill)
        {
            AddElementsBill(bill);

            _serviceData.InsertOne(bill);
        }

        private string GetState(BillControl bill)
        {
            if (!(bill?.Months?.Any() ?? false))
                return States.PAGO.ToString();
            int presentMonth = int.Parse(DateTime.Now.ToString("MM"));

            // Se obtienen los meses anteriores y actual

            List<MonthControl> months = new List<MonthControl>();

            for (int index=presentMonth; index >= 1; index--)
            {
                MonthControl month = bill.Months.FirstOrDefault(month => 
                    DateTime.ParseExact(month.Name, "MMMM", CultureInfo.CurrentCulture).Month == index);
                if (month != null)
                    months.Add(month);
            }

            if (months.Any(month => !month.IsPaid))
            {
                if (presentMonth == DateTime.ParseExact(months.First(month => 
                    !month.IsPaid).Name, "MMMM", CultureInfo.CurrentCulture).Month )
                    bill.State = States.PENDIENTE.ToString();
                else
                    bill.State = States.ATRASADO.ToString();
            }
            else
            {
                bill.State = States.PAGO.ToString();
            }
            return bill.State;
        }

        private void AddElementsBill(BillControl bill)
        {
            if (string.IsNullOrWhiteSpace(bill.State))
                bill.State = States.PENDIENTE.ToString();
            if (!(bill.Months?.Any() ?? false))
                bill.Months = GetDefaultMonths();
        }

        private IEnumerable<MonthControl> GetDefaultMonths()
        {
            IEnumerable<MonthControl> months = Enumerable.Empty<MonthControl>();
            foreach (string nameMonth in DateTimeFormatInfo.CurrentInfo.MonthNames)
            {
                if (string.IsNullOrWhiteSpace(nameMonth))
                    continue;
                MonthControl month = new MonthControl
                {
                    IsPaid = false,
                    Name = nameMonth.ToUpper()
                };
                months = months.Append(month);
            }

            return months;
        }
    }
}
