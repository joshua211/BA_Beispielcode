using System.Collections.Immutable;
using BeispielProjekt;
using Beispielprojekt.Domain;
using ErrorOr;

namespace Beispielprojekt;

public class Bestellung
{
    public BestellungsId Id { get; private set; }
    public Status Genehmigungsstatus { get; private set; }
    public ImmutableArray<Artikel> Positionen { get; private set; }
    public MitarbeiterId Auftraggeber { get; private set; }

    private Bestellung(BestellungsId id, Status genehmigungsstatus, ImmutableArray<Artikel> positionen,
        MitarbeiterId auftraggeber)
    {
        Id = id;
        Genehmigungsstatus = genehmigungsstatus;
        Positionen = positionen;
        Auftraggeber = auftraggeber;
    }


    public static Bestellung NeueBestellung(Artikel[] positionen, MitarbeiterId auftraggeber)
    {
        return new Bestellung(BestellungsId.Neu(), Status.Eingereicht, positionen.ToImmutableArray(), auftraggeber);
    }

    public Preis BerechneGesamtwert()
    {
        Preis gesamt = new Preis(0);
        foreach (var pos in Positionen)
        {
            gesamt += pos.Preis;
        }

        return gesamt;
    }

    public ErrorOr<Success> VonAbteilungGenehmigen()
    {
        if (Genehmigungsstatus != Status.Eingereicht)
            return Error.Unexpected(description: $"Die Bestellung befindet sich nicht im Status {Status.Eingereicht}");

        Genehmigungsstatus = Status.AbtGenehmigt;
        return new Success();
    }

    public ErrorOr<Success> VonAbteilungAblehnen()
    {
        if (Genehmigungsstatus != Status.Eingereicht)
            return Error.Unexpected(description: $"Die Bestellung befindet sich nicht im Status {Status.Eingereicht}");

        Genehmigungsstatus = Status.AbtAbgelehnt;
        return new Success();
    }

    public bool IstImBudget(Preis restlichesBudget)
    {
        return restlichesBudget >= BerechneGesamtwert();
    }

    public ErrorOr<Success> Bestätigen(IMitarbeiterRepository mitarbeiterRepository, IBestellungsRepository bestellungsRepository)
    {
        var mitarbeiter = mitarbeiterRepository.GetById(Auftraggeber);
        var restlichesBudget = mitarbeiter.GetRestlichesBudget(bestellungsRepository);

        if (!IstImBudget(restlichesBudget))
            return Errors.UnzureichendesBudget();

        if (Genehmigungsstatus != Status.GfGenehmigt)
            return Errors.FalscherGenehmigungsstatus(Status.GfGenehmigt);

        Genehmigungsstatus = Status.Abgeschlossen;
        return new Success();
    }

    public ErrorOr<Success> Stornieren()
    {
        throw new NotImplementedException();
    }
}