namespace BeispielProjekt;

public interface IBudgetService
{
    bool IstImBudget(Mitarbeiter mitarbeiter, Bestellung bestellung);
}

public class BudgetService : IBudgetService
{
    private readonly IBestellungsRepository bestellungsRepository;
    private readonly IArtikelRepository artikelRepository;

    public BudgetService(IBestellungsRepository bestellungsRepository, IArtikelRepository artikelRepository)
    {
        this.bestellungsRepository = bestellungsRepository;
        this.artikelRepository = artikelRepository;
    }


    public bool IstImBudget(Mitarbeiter mitarbeiter, Bestellung bestellung)
    {
        Preis bestellungsPreis = GetBestellungsPreis(bestellung);

        Preis verbrauchtesBudget = new Preis(0);
        foreach(var bestellungsId in mitarbeiter.Bestellungen)
        {
            var best = bestellungsRepository.GetById(bestellungsId);
            if(best.Genehmigungsstatus is Status.AbtAbgelehnt or Status.GfAbgelehnt or Status.Storniert)
                continue;
            verbrauchtesBudget += GetBestellungsPreis(best);
        }

        return (mitarbeiter.MaxBudget - verbrauchtesBudget) >= bestellungsPreis;
    }

    private Preis GetBestellungsPreis(Bestellung bestellung)
    {
        Preis bestellungsPreis = new Preis(0);
        foreach (var id in bestellung.Positionen)
        {
            var artikel = artikelRepository.GetById(id);
            bestellungsPreis += artikel.Preis;
        }

        return bestellungsPreis;
    }
}
