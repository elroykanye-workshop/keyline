using Firebase.Database;
using Firebase.Database.Query;
using keyline.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace keyline.Helper
{
    class AddCategoryData
    {
        public List<Category> Categories { get; set; }
        private readonly FirebaseClient firebaseClient;
        private readonly string databaseEnpoint = Constants.firebaseEndpoint;

        public AddCategoryData()
        {
            firebaseClient = new FirebaseClient(databaseEnpoint);
            Categories = new List<Category>()
             {
                 new Category()
                 {
                     CategoryId = 1,
                     CategoryName = "Burger",
                     CategoryPoster = "MainBurger",
                     ImageUrl = "burger.png"
                 },
                 new Category()
                 {
                     CategoryId = 2,
                     CategoryName = "Pizza",
                     CategoryPoster = "MainPizza",
                     ImageUrl = "pizza.png"
                 },
                 new Category()
                 {
                     CategoryId = 3,
                     CategoryName = "Desserts",
                     CategoryPoster = "MainDesserts",
                     ImageUrl = "desserts.png"
                 },
             };
        }

        public async Task AddCategoriesAsync()
        {
            try
            {
                foreach(var category in Categories)
                {
                    await firebaseClient.Child("Categories")
                        .PostAsync(new Category()
                        {
                            CategoryId = category.CategoryId,
                            CategoryName = category.CategoryName,
                            CategoryPoster = category.CategoryPoster,
                            ImageUrl = category.ImageUrl
                        });
                }
            }
            catch (Exception exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", exception.Message, "Okay");
            }
        }

    }
}
