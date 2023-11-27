namespace MyReference.View;

public partial class DatabaseCarPage : ContentPage
{
    public DatabaseCarPage(DatabaseCarViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}