using keyline.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace keyline.Helper
{
    class AddFoodItemsData
    {
        public List<FoodItem> FoodItems { get; set; }

        public AddFoodItemsData()
        {
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
    }
}
