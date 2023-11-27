

namespace MyReference.ViewModel;

using Newtonsoft.Json;
using System.Collections.ObjectModel;

public partial class DatabaseCarViewModel : BaseViewModel
{
    UserManagementService MyDBServices;

    public DatabaseCarViewModel(UserManagementService MyDBServices)
    {
        this.MyDBServices = MyDBServices;
        MyDBServices.ConfigTools();
    }

    public ObservableCollection<CarForDb> ShownList { get; set; } = new();


      // On utilse pas le read On utilise que le Fill, Insert et Delete

/*    async Task ReadAccess()
    {
        Globals.UserSet.Tables["Access"].Clear();

        try
        {
            ShownList.Clear();
            await MyDBServices.ReadCarsTable(); // Then read the table
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }
    }*/


    [RelayCommand]
    async Task FillFromDB()
    {
        IsBusy = true;

        List<CarForDb> MyList = new();

        ShownList.Clear();
        await MyDBServices.FillFromCarsDBTable(); // Appeler la nouvelle méthode pour les voitures
        try
        {
            MyList = Globals.UserSet.Tables["Voitures"].AsEnumerable().Select(m => new CarForDb()
            {
                VoitureID = m.Field<Int32>("VoituresID"),
                Marque = m.Field<string>("Marque"),
                Modele = m.Field<string>("Modele"),
                Annee = m.Field<Int32?>("Annee") ?? 0,

            }).ToList();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "OK");
        }

        foreach (var item in MyList)
        {
            ShownList.Add(item);
        }

        IsBusy = false;
    }

    [RelayCommand]
    async Task FillInDbFromJson()
    {
        try
        {
            // Lire le fichier JSON
            string jsonData = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DataFromDesktop/carForDbJson.json"));

            // Convertir les données JSON en objets
            List<CarForDb> carDataList = JsonConvert.DeserializeObject<List<CarForDb>>(jsonData);

            // Insérer les données dans la base de données
            foreach (CarForDb carData in carDataList)
            {
                MyDBServices.InsertCar(carData.Marque, carData.Modele, carData.Annee);
            }

            // Afficher une alerte pour indiquer que l'insertion s'est terminée avec succès
            await Shell.Current.DisplayAlert("Database", "Data from JSON inserted successfully", "Ok");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
        }
    }


    [RelayCommand]
    async Task FillInDbYourCar()
    {
        try
        {

            // Afficher une interface utilisateur pour permettre à l'utilisateur de saisir les données de la voiture
            string marque = await Shell.Current.DisplayPromptAsync("Insert your car", "Enter Marque");
            string modele = await Shell.Current.DisplayPromptAsync("Insert your car", "Enter Modele");
            string anneeStr = await Shell.Current.DisplayPromptAsync("Insert your car", "Enter Annee");
            int annee = int.Parse(anneeStr);

            MyDBServices.InsertCar(marque, modele, annee);


            // Afficher une alerte pour indiquer que l'insertion s'est terminée avec succès
            await Shell.Current.DisplayAlert("Database", "Data from JSON inserted successfully", "Ok");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
        }
    }

    [RelayCommand]
    async Task DeleteCarFromDb()
    {
        try
        {
            // Demandez à l'utilisateur l'ID de la voiture à supprimer
            string voitureIDStr = await Shell.Current.DisplayPromptAsync("Delete your car", "Enter Voiture ID");
            int voitureID = int.Parse(voitureIDStr);

            await MyDBServices.DeleteCar(voitureID);

            // Après la suppression, mettez à jour la liste affichée
            await FillFromDB();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Database", ex.Message, "Ok");
        }
    }

}
