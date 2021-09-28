using Firebase.Database;
using Firebase.Database.Query;
using keyline.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keyline.Service
{
    class UserService
    {
        FirebaseClient firebaseClient;
        private string databaseEndpoint = "https://keyline-17e32-default-rtb.firebaseio.com/"

        public UserService()
        {
            firebaseClient = new FirebaseClient(databaseEndpoint);
        }

        public async Task<bool> IsUserExists(string username)
        {
            var user = (await firebaseClient.Child("Users")
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
