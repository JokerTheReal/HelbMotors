namespace MyReference.Model;

public class Car
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int StartProduction { get; set; }
    public string Image { get; set; }
    public string Marque { get; set; }
    public string Color { get; set; }

    //Je met les infos suivant pour la db 
    public int VoitureID { get; set; }
    public string Modele { get; set; }
    public int Annee { get; set; }
}


