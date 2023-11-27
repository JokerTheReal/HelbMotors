namespace MyReference.ViewModel
{
    public partial class SettingsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string monTxt;

        public SettingsViewModel()
        {
            MonTxt = "Paramètres !";

            Preferences.Default.Get("one", "Enes");
        }
    }
}
