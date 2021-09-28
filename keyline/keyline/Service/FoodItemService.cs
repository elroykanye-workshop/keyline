using Firebase.Database;
using keyline.Helper;
using keyline.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace keyline.Service
{
    class FoodItemService
    {
        private readonly FirebaseClient firebaseClient;
        private readonly string databaseEndpoint = Constants.firebaseEndpoint;

        public FoodItemService()
        {
            firebaseClient = new FirebaseClient(databaseEndpoint);
        }

        public async Task<List<FoodItem>> GetFoodItemsAsync()
        {
            var foodItems = (await firebaseClient.Child("FoodItems")
                .OnceAsync<FoodItem>())
                .Select(f => new FoodItem
                {
                    CategoryID = f.Object.CategoryID,
                    Description = f.Object.Description,
                    HomeSelected = f.Object.HomeSelected,
                    ImageUrl = f.Object.ImageUrl,
                    Name = f.Object.Name,
                    Price = f.Object.Price,
                    ProductID = f.Object.ProductID,
                    Rating = f.Object.Rating,
                    RatingDetail = f.Object.RatingDetail,
                }).ToList();

            return foodItems;
        }

        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByCategoryIdAsync(int categoryId)
        {
            ObservableCollection<FoodItem> foodItemsByCategory = new ObservableCollection<FoodItem>();

            List<FoodItem> items = (await GetFoodItemsAsync())
                .Where(f => f.CategoryID == categoryId).ToList();

            foreach(var item in items)
            {
                foodItemsByCategory.Add(item);
            }

            return foodItemsByCategory;
        }

        public async Task<ObservableCollection<FoodItem>> GetLatestFoodItemsAsync()
        {
            var latestFoodItems = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync())
                .OrderByDescending(f => f.ProductID).Take(3);
            foreach (var item in items)
            {
                latestFoodItems.Add(item);
            }
            return latestFoodItems;
        }

    }
}
