namespace MyReference.ViewModel;


public partial class DetailViewModel : BaseViewModel
{
    private string matchMessage;
    private string imageUrl;

    private string image;
    public string Image
    {
        get { return image; }
        set
        {
            if (image != value)
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
    }

    public string MatchMessage
    {
        get { return matchMessage; }
        set { SetProperty(ref matchMessage, value); }
    }

    public string ImageUrl
    {
        get { return imageUrl; }
        set
        {
            if (imageUrl != value)
            {
                imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }
    }

    [ObservableProperty]
    private Model.Car matchingCar;
 

    [ObservableProperty]
    string monTxt;

    DeviceOrientationServices MyDeviceOrientationServices;

    public DetailViewModel()
    {
        this.MyDeviceOrientationServices = new DeviceOrientationServices();
        MyDeviceOrientationServices.ConfigureScanner();

        MyDeviceOrientationServices.SerialBuffer.Changed += SerialBuffer_Changed;
    }

    private void SerialBuffer_Changed(object sender, EventArgs e)
    {
        DeviceOrientationServices.QueueBuffer myQueue = (DeviceOrientationServices.QueueBuffer)sender;

        MonTxt = myQueue.Dequeue().ToString();

        foreach (Model.Car m in Globals.MyStaticList)
        {
            if (this.MonTxt == m.Id.ToString())
            {
                MatchMessage =
                    " Id : " + this.MonTxt +
                    "\n Brand : " + m.Marque +
                    "\n Modele : " + m.Title +
                    "\n Prdouction date : " + m.StartProduction +
                    "\n Color : " + m.Color;
                ;
                this.MatchingCar = m;
            }
        }
    }
}