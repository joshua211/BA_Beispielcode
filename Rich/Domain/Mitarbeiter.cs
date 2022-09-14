using Beispielprojekt;

namespace BeispielProjekt;

public class Mitarbeiter
{
    public MitarbeiterId Id { get; private set; }
    public Rolle Rolle { get; private set; }
    public Preis MaxBudget { get; private set; }
    public BestellungsId[] Bestellungen { get; private set; }

    private Mitarbeiter(MitarbeiterId id, Rolle rolle, Preis maxBudget, BestellungsId[] bestellungen)
    {
        Id = id;
        Rolle = rolle;
        MaxBudget = maxBudget;
        Bestellungen = bestellungen;
    }

    public static Mitarbeiter NeuerMitarbeiter(Rolle rolle, Preis maxBudget)
    {
        return new Mitarbeiter(new MitarbeiterId(Guid.NewGuid().ToString()), rolle, maxBudget,
            Array.Empty<BestellungsId>());
    }

    public Preis GetRestlichesBudget(IBestellungsRepository bestellungsRepository)
    {
        var verbrauchtesBudget = new Preis(0);
        foreach (var bestellungsId in Bestellungen)
        {
            var bestellung = bestellungsRepository.GetById(bestellungsId);
            verbrauchtesBudget += bestellung.BerechneGesamtwert();
        }

        return MaxBudget - verbrauchtesBudget;
    }
}

public enum Rolle
{
    Mitarbeiter,
    Abteilungsleiter,
    Geschäftsführer
}