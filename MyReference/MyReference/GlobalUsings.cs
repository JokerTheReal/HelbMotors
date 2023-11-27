global using MyReference.View;
global using MyReference.ViewModel;
global using MyReference.Model;
global using MyReference.Services;

global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;


global using System.Diagnostics;
global using System.Collections.ObjectModel;
global using System.Text.Json;

global using System.Data;
//global using CommunityToolkit.Maui;

public class Globals
{
    public static List<MyReference.Model.Car> MyStaticList = new List<MyReference.Model.Car>();
    public static DataSet UserSet = new(); //UserSet n'existe pas
}
