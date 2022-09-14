using Beispielprojekt;

namespace BeispielProjekt;

public interface IBestellungsRepository
{
    Bestellung GetById(BestellungsId id);
    void SpeicherBestellung(Bestellung neueBestellung);
    void UpdateBestellung(Bestellung bestellung);
}