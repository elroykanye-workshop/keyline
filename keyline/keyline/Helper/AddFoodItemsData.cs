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
    class AddFoodItemsData
    {
        FirebaseClient firebaseCient;
        private readonly string databaseEndpoint = Constants.firebaseEndpoint;

        public List<FoodItem> FoodItems { get; set; }

        public AddFoodItemsData()
        {
            firebaseCient = new FirebaseClient(databaseEndpoint);
            FoodItems = new List<FoodItem>()
            {
                new FoodItem
                {
                    ProductID = 1,
                    CategoryID = 1,
                    ImageUrl = "MainBurger",
                    Name = "Burger and Pizza Hub 1",
                    Description = "Burger - Pizza - Breakfast",
                    Rating = "4.8",
                    RatingDetail = "(121 ratings)",
                    HomeSelected = "CompleteHeart",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 2,
                    CategoryID = 1,
                    ImageUrl = "MainBurger",
                    Name = "Burger and Pizza Hub 2",
                    Description = "Burger - Pizza - Breakfast",
                    Rating = "4.8",
                    RatingDetail = "(121 ratings)",
                    HomeSelected = "EmptyeHeart",
                    Price = 45
                }
            };
        }

        public async Task AddFoodItemsAsync()
        {
            try
            {
                foreach (FoodItem foodItem in FoodItems)
                {
                    await firebaseCient.Child("FoodItems")
                        .PostAsync(new FoodItem()
                        {
                            ProductID = foodItem.ProductID,
                            ImageUrl = foodItem.ImageUrl,
                            Name = foodItem.Name,
                            Description = foodItem.Description,
                            Rating = foodItem.Rating,
                            RatingDetail = foodItem.RatingDetail,
                            HomeSelected = foodItem.HomeSelected,
                            Price = foodItem.Price,
                            CategoryID = foodItem.CategoryID
                        });
                }

            } 
            catch(Exception exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", exception.Message, "Okay");
            }
        }

    }
}
