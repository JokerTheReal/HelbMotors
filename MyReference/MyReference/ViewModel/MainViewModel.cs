namespace MyReference.ViewModel;
using MyReference.ViewModel;
using Newtonsoft.Json;
using System.Windows.Input;
using static MyReference.MainPage;

public partial class MainViewModel : BaseViewModel
{
    

    [ObservableProperty]
    string monTexte = "Scan a barcode to find a car";
    public ObservableCollection<Model.Car> MyShownList { get; } = new();


    [ObservableProperty]
    public string monCode; //Génére un get set (commencer avec une minuscule)

    public MainViewModel()
	{
        string MyPreference = Preferences.Default.Get("one", "Unknow");
        CreateUserDataTables MyUserTables = new();
        //Globals.UserSet.Tables["Users"].Columns["UserName"] = "blabla"; 
    }

    [RelayCommand]
    public async Task GoToDetailPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(DetailPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }

    [RelayCommand]
    public async Task GoToAboutPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(AboutPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }

    [RelayCommand]
    public async Task GoToDetailMyNewPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(AboutPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }

    [RelayCommand]
    public async Task GoToSettingsPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
    }

    [RelayCommand]
    public async Task GoToDatabaseCarPage(string data)
    {
        try {
            await Shell.Current.GoToAsync(nameof(DatabaseCarPage), true, new Dictionary<string, object>
        {
            {"Databc", data }
        });
        } catch (Exception ex) {
            await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
        }

    }


    [RelayCommand]
    async Task CarsFromJSON()
    {
        if (IsBusy) return;

        CarService MyService = new CarService();

        try
        {
            IsBusy = true;
            Globals.MyStaticList = await MyService.GetCars();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get Students: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally { IsBusy = false; }

        MyShownList.Clear();

        foreach (Model.Car stu in Globals.MyStaticList)
        {
            MyShownList.Add(stu);
        }
    }
}