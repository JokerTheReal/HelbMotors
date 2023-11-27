using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyReference.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        private string monTxt;

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }
        public string MonTxt
        {
            get => monTxt;
            set => SetProperty(ref monTxt, value);
        }

        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public ICommand ConfirmCommand { get; }

        public LoginViewModel()
        {
            MonTxt = "Welcome Back Admin !";
            ConfirmCommand = new Command(async () => await ConfirmLogin());
        }

        private async Task ConfirmLogin()
        {
            if (Username == "admin" && Password == "helb1234" || Username == "Admin" && Password == "helb1234") 
            {
                try
                {
                    await Shell.Current.GoToAsync(nameof(DatabaseCarPage));

                    if (Username == "admin")
                    {
                        await Shell.Current.DisplayAlert("Bienvenue", "Welcome admin!", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
                }
            }
            else
            {
                string errorMessage = "Invalid username or password";
                DisplayErrorMessage(errorMessage);
            }
        }

        private void DisplayErrorMessage(string message)
        {
            ErrorMessage = message;
        }
    }
}
