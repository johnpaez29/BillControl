using DataAccess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IServiceData<T>
    {
        Task<T> GetById(string id);

        Task<IEnumerable<T>> GetAllAsync();

        void InsertOne(T element);

        void Update(T Element);

        void Delete(string id);
    }
}
