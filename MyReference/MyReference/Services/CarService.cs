namespace MyReference.Services;

public class CarService : ContentPage
{
	public CarService()
	{
	
	}

	public async Task<List<Model.Car>> GetCars()
    {
        List<Model.Car> cars;

        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DataFromDesktop/carjson.json");
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

        using var reader = new StreamReader(stream);

        var contents = await reader.ReadToEndAsync();
        cars = JsonSerializer.Deserialize<List<Model.Car>>(contents);

        return cars;
    }
}