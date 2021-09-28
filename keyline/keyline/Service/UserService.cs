using Firebase.Database;
using Firebase.Database.Query;
using keyline.Helper;
using keyline.Model;
using System.Linq;
using System.Threading.Tasks;

namespace keyline.Service
{
    class UserService
    {
        private readonly FirebaseClient firebaseClient;
        private readonly string databaseEndpoint = Constants.firebaseEndpoint;

        public UserService()
        {
            firebaseClient = new FirebaseClient(databaseEndpoint);
        }

        public async Task<bool> IsUserExists(string username)
        {
            FirebaseObject<User> user = (await firebaseClient.Child("Users")
                .OnceAsync<User>()).Where(u => u.Object.Username.Equals(username))
                .FirstOrDefault();

            return user != null;
        }

        public async Task<bool> RegisterUser(string username, string password)
        {
            if (await IsUserExists(username) == false)
            {
                await firebaseClient.Child("Users")
                    .PostAsync(new User()
                    {
                        Username = username,
                        Password = password
                    });
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LoginUser(string username, string password)
        {
            var user = (await firebaseClient.Child("Users")
                .OnceAsync<User>())
                .Where(u => u.Object.Username == username)
                .Where(u => u.Object.Password == password)
                .FirstOrDefault();

            return user != null; 
        }
    }
}
