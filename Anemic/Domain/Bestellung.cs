namespace BeispielProjekt;

public class Bestellung
{
    public BestellungsId Id { get; set; }
    public Status Genehmigungsstatus { get; set; }
    public Artikelid[] Positionen { get; set; }
    public MitarbeiterId Auftraggeber { get; set; }
}
