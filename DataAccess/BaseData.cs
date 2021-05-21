using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class BaseData
    {
        public FirebaseClient FirebaseClient { get; set; }

        public BaseData()
        {
            FirebaseClient = new FirebaseClient(AppSettings.ConnectionString);
        }
    }
}
