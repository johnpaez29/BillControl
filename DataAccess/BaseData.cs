using Firebase.Database;

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
