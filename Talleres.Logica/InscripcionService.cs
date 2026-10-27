using Talleres.Logica.Data;

namespace Talleres.Logica;

public class InscripcionService
{
    private const int TOPE_TALLERES_UCSE = 2;

    private readonly TallerRepository _tallerRepository;
    private readonly InscripcionRepository _inscripcionRepository;

    public InscripcionService()
        : this(new TallerRepository("talleres.json"), new InscripcionRepository("inscripciones.json"))
    {
    }

    public InscripcionService(TallerRepository tallerRepository, InscripcionRepository inscripcionRepository)
    {
        _tallerRepository = tallerRepository;
        _inscripcionRepository = inscripcionRepository;
        _tallerRepository.CrearArchivoSiNoExiste();
    }
}
