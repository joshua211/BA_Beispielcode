namespace BeispielProjekt;

public class Mitarbeiter
{
    public MitarbeiterId Id { get; set; }
    public Rolle Rolle { get; set; }
    public Preis MaxBudget { get; set; }
    public BestellungsId[] Bestellungen { get; set; }
}

public enum Rolle
{
    Mitarbeiter,
    Abteilungsleiter,
    Geschäftsführer
}