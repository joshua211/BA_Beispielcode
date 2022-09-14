using Beispielprojekt;

namespace BeispielProjekt;


public interface IMitarbeiterRepository
{
    Mitarbeiter GetById(MitarbeiterId id);
}
