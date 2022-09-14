namespace BeispielProjekt;

public interface IBestellungsService
{
    BestellungsId StarteBestellung(MitarbeiterId id, Artikelid[] artikel);
}

public class BestellungsService : IBestellungsService
{
    private readonly IBudgetService budgetService;
    private readonly IMitarbeiterRepository mitarbeiterRepository;
    private readonly IBestellungsRepository bestellungsRepository;
    private readonly IEmailService emailService;

    public BestellungsService(IBudgetService budgetService, IMitarbeiterRepository mitarbeiterRepository, IEmailService emailService, IBestellungsRepository bestellungsRepository)
    {
        this.budgetService = budgetService;
        this.mitarbeiterRepository = mitarbeiterRepository;
        this.emailService = emailService;
        this.bestellungsRepository = bestellungsRepository;
    }

    public BestellungsId StarteBestellung(MitarbeiterId id, Artikelid[] artikel)
    {
        var mitarbeiter = mitarbeiterRepository.GetById(id);
        Bestellung neueBestellung = new Bestellung
        {
            Auftraggeber = id,
            Genehmigungsstatus = Status.Eingereicht,
            Id = new BestellungsId(Guid.NewGuid().ToString()),
            Positionen = artikel
        };

        if (!budgetService.IstImBudget(mitarbeiter, neueBestellung))
            throw new NotEnoughBudgetException();
        
        bestellungsRepository.SpeicherBestellung(neueBestellung);
        emailService.BenachrichtigeAbteilungsleiter(neueBestellung.Id);

        return neueBestellung.Id;
    }
}