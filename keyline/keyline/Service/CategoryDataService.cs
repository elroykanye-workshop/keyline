using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Firebase.Database.Query;
using Firebase.Database;
using keyline.Helper;
using keyline.Model;
using System.Threading.Tasks;

namespace keyline.Service
{
    class CategoryDataService
    {
        private readonly FirebaseClient firebaseClient;
        private readonly string databaseEndpoint = Constants.firebaseEndpoint;

        public CategoryDataService ()
        {
            firebaseClient = new FirebaseClient(databaseEndpoint);

        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            List<Category> categories = (await firebaseClient.Child("Categories")
                .OnceAsync<Category>())
                .Select(c => new Category
                {
                    CategoryId = c.Object.CategoryId,
                    CategoryName = c.Object.CategoryName,
                    CategoryPoster = c.Object.CategoryPoster,
                    ImageUrl = c.Object.ImageUrl
                }).ToList();
            return categories;
        }
    }
}
