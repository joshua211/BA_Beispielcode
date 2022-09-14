using Beispielprojekt;
using Beispielprojekt.Domain;
using ErrorOr;

namespace BeispielProjekt;

public interface IBestellungsService
{
    ErrorOr<BestellungsId> StarteBestellung(MitarbeiterId id, Artikel[] artikel);
    ErrorOr<Success> BestätigeBestellung(BestellungsId id, MitarbeiterId genehmiger);
}

public class BestellungsService : IBestellungsService
{
    private readonly IMitarbeiterRepository mitarbeiterRepository;
    private readonly IBestellungsRepository bestellungsRepository;
    private readonly IArtikelRepository artikelRepository;
    private readonly IEmailService emailService;

    public BestellungsService(IMitarbeiterRepository mitarbeiterRepository, IEmailService emailService,
        IBestellungsRepository bestellungsRepository, IArtikelRepository artikelRepository)
    {
        this.mitarbeiterRepository = mitarbeiterRepository;
        this.emailService = emailService;
        this.bestellungsRepository = bestellungsRepository;
        this.artikelRepository = artikelRepository;
    }

    public ErrorOr<BestellungsId> StarteBestellung(MitarbeiterId id, Artikel[] artikel)
    {
        var mitarbeiter = mitarbeiterRepository.GetById(id);
        Preis restlichesBudget = mitarbeiter.GetRestlichesBudget(bestellungsRepository);

        var bestellung = Bestellung.NeueBestellung(artikel, id);
        if (!bestellung.IstImBudget(restlichesBudget))
            return Errors.UnzureichendesBudget();
        
        bestellungsRepository.SpeicherBestellung(bestellung);
        emailService.BenachrichtigeAbteilungsleiter(bestellung.Id);
        
        return bestellung.Id;
    }

    public ErrorOr<Success> BestätigeBestellung(BestellungsId id, MitarbeiterId genehmiger)
    {
        var bestellung = bestellungsRepository.GetById(id);
        var result = bestellung.Bestätigen(mitarbeiterRepository, bestellungsRepository);
        if (result.IsError)
            return result.FirstError;
        
        bestellungsRepository.UpdateBestellung(bestellung);
        return new Success();
    }
}