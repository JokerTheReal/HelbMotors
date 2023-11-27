namespace MyReference.ViewModel
{
    public partial class WelcomeViewModel : BaseViewModel
    {
        private string _monTxt;

        public string MonTxt
        {
            get { return _monTxt; }
            set
            {
                _monTxt = value;
                OnPropertyChanged();
            }
        }


        public WelcomeViewModel()
        {
            MonTxt = "Bienvenue sur la Welcome page !";

        }

        [RelayCommand]
        public async Task GoToLoginPage()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
        }

        [RelayCommand]
        public async Task GoToMainPage()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(MainPage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", ex.Message, "Ok");
            }
        }
    }
}
