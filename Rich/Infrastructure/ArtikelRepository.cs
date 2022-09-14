using Beispielprojekt;

namespace BeispielProjekt;


public interface IArtikelRepository
{
    Artikel GetById(ArtikelId id);
}
