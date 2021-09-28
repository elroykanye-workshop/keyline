using keyline.Model;
using keyline.Service;
using keyline.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace keyline.ViewModel
{
    class ProductViewModel: BaseViewModel
    {
        private string _Username;
        public string Username
        {
            get 
            { 
                return _Username; 
            }
            set 
            { 
                _Username = value;
                OnPropertyChanged();
            }
        }

        private int _UserCartItemsCount;
        public int UserCartItemsCount
        {
            get { return _UserCartItemsCount; }
            set
            { 
                _UserCartItemsCount = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<FoodItem> LatestItems { get; set; }

        public Command ViewCartCommand { get; set; }
        public Command LogoutCommand { get; set; }

        public ProductViewModel()
        {
            var username = Preferences.Get("Üsername", String.Empty);
            if (String.IsNullOrEmpty(username))
            {
                Username = "Guest";
            }
            else
            {
                Username = username;
            }

            UserCartItemsCount = new CartItemService().GetUserCartCount();

            Categories = new ObservableCollection<Category>();
            LatestItems = new ObservableCollection<FoodItem>();


            ViewCartCommand = new Command(async () => await VIewCartAsync());
            LogoutCommand = new Command(async () => await LogoutAsync());


            GetCategories();
            GetLatestItems();
        }

        private async Task LogoutAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new LogoutView());
        }

        private async Task VIewCartAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
        }

        private async void GetLatestItems()
        {
            var foodItems = await new FoodItemService().GetLatestFoodItemsAsync();
            LatestItems.Clear();
            foreach(var item in foodItems)
            {
                LatestItems.Add(item);
            }
        }

        private async void GetCategories()
        {
            var categories = await new CategoryDataService().GetCategoriesAsync();
            Categories.Clear();
            foreach (var cat in categories)
            {
                Categories.Add(cat);
            }
        }
    }
}
