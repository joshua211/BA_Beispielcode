namespace BeispielProjekt;


public interface IMitarbeiterRepository
{
    Mitarbeiter GetById(MitarbeiterId id);
}
