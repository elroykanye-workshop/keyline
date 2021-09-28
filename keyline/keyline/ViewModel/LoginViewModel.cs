using keyline.Service;
using keyline.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace keyline.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        private string _Username;
        public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                this._Username = value;
                OnPropertyChanged();
            }
        }

        private string _Password;
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
                OnPropertyChanged();
            }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get
            {
                return this._IsBusy;
            }
            set
            {
                this._IsBusy = value;
                OnPropertyChanged();
            }
        }

        private bool _Result;
        public bool Result
        {
            get
            {
                return this._Result;
            }
            set
            {
                this._Result = value;
                OnPropertyChanged();
            }
        }


        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
        }

        private async Task RegisterCommandAsync()
        {
            // throw new NotImplementedException();
            if (IsBusy) { return; }
            try
            {
                
                bool fieldEmpty = IsFieldEmpty(Username, Password);
                if (fieldEmpty)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Username or password cannot be empty", "Try again");
                }
                else
                {
                    IsBusy = true;

                    UserService userService = new UserService();

                    Result = await userService.RegisterUser(Username, Password);

                    if (Result)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", "Account registered", "Okay");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error ", "Account already exists with this username", "Okay");
                    }
                }
            }
            catch (Exception exception)
            {
                await Application.Current.MainPage.
                    DisplayAlert("Error", exception.Message, "Okay");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoginCommandAsync()
        {
            // throw new NotImplementedException();
            if (IsBusy) { return; }
            try
            {
                bool fieldEmpty = IsFieldEmpty(Username, Password);
                if (fieldEmpty)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Username or password cannot be empty", "Try again");
                }
                else
                {
                    IsBusy = true;

                    UserService userService = new UserService();

                    Result = await userService.LoginUser(Username, Password);

                    if (Result)
                    {
                        Preferences.Set("Username", Username);
                        await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password", "Okay");
                    }
                }
            }
            catch (Exception exception)
            {
                await Application.Current.MainPage.
                    DisplayAlert("Error", exception.Message, "Okay");
            }
            finally
            {

            }
        }

        private bool IsFieldEmpty(string username, string password)
        {
            bool flag = Username == "" || Password == "" || Username == null || Password == null;
            return flag;
        }
    }
}
