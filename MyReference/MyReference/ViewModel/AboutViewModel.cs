
namespace MyReference.ViewModel
{
    public partial class AboutViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string monTxt;

        public AboutViewModel()
        {
            MonTxt = "Bienvenue sur la page À propos !";
        }
    }
}

