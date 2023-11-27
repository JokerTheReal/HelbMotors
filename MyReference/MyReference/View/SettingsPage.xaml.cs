
namespace MyReference.View;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(AboutViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        string myPref = Preferences.Default.Get("one", "Unknow");

        if (myPref == "Enes")
        {
            //blabla.BackgroundColor = Color.FromRgb(0, 233, 50);
        }

    }
}