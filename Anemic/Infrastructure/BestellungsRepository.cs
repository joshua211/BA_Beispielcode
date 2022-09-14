namespace BeispielProjekt;


public interface IBestellungsRepository
{
    Bestellung GetById(BestellungsId id);
    void SpeicherBestellung(Bestellung neueBestellung);
}
