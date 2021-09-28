using keyline.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace keyline.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        async void ButtonCategories_Clicked(object sender, EventArgs e)
        {
            var addCatData = new AddCategoryData();
            await addCatData.AddCategoriesAsync();
        }

        async void ButtonProducts_Clicked(object sender, EventArgs e)
        {
            var addFdiData = new AddFoodItemsData();
            await addFdiData.AddFoodItemsAsync();

        }
    }
}