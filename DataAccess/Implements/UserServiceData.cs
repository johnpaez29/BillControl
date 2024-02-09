using DataAccess.Interfaces;
using DataAccess.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Implements
{
    public class UserServiceData : BaseData, IServiceData<User>
    {
        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var dbUsers = await FirebaseClient.Child("Users").OnceAsync<User>();

            IEnumerable<User> users = dbUsers.ToList().Select(dbBill => new User
            {
                IdUser = dbBill.Key,
                Name = dbBill.Object.Name,
                LastName = dbBill.Object.LastName,
                Image = dbBill.Object.Image
            });

            return users;
        }

        public Task<User> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task InsertOne(User element)
        {
            throw new NotImplementedException();
        }

        public Task Update(User Element)
        {
            throw new NotImplementedException();
        }
    }
}
