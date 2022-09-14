namespace BeispielProjekt
{
    public interface IEmailService
    {
         void BenachrichtigeAbteilungsleiter(BestellungsId bestellungsId);
         void BenachrichteGeschäftsführung(BestellungsId bestellungsId);
    }
}