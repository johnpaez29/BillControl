using DataAccess.Models;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BillServiceData : BaseData, IServiceData<BillControl>
    {
        public async void Delete(string id)
        {
            await FirebaseClient.Child("Bills").Child(id).DeleteAsync();
        }

        public async Task<IEnumerable<BillControl>> GetAllAsync()
        {
            var dbBills = await FirebaseClient.Child("Bills").OnceAsync<BillControl>();

            IEnumerable<BillControl> bills = dbBills.ToList().Select(dbBill => new BillControl 
            {
                Id = dbBill.Key,
                Name = dbBill.Object.Name,
                Months = dbBill.Object.Months,
                PayDay = dbBill.Object.PayDay
            });

            return bills;
        }

        public async Task<BillControl> GetById(string id)
        {
            var dbBill = await FirebaseClient.Child("Bills").Child(id).OnceSingleAsync<BillControl>();

            BillControl bill = new BillControl
            {
                Id = id,
                Name = dbBill.Name,
                Months = dbBill.Months
            };

            return bill;
        }

        public async void InsertOne(BillControl element)
        {
            await FirebaseClient.Child("Bills").PostAsync(element);
        }

        public async void Update(BillControl Element)
        {
            string id = Element.Id;
            Element.Id = null;
            await FirebaseClient.Child("Bills").Child(id).PutAsync(Element);
        }
    }
}
