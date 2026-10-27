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

    public List<Taller> ObtenerTalleres()
    {
        var talleres = _tallerRepository.Leer();
        var inscripcionesActivas = _inscripcionRepository.Leer().Where(i => i.EstaActiva()).ToList();

        foreach (var taller in talleres)
        {
            var activasTaller = inscripcionesActivas.Count(i => i.TallerId == taller.Id);
            taller.CuposDisponibles = taller.Cupo - activasTaller;
        }

        return talleres;
    }

    public List<Inscripcion> ObtenerInscripciones(int? dni = null)
    {
        var inscripciones = _inscripcionRepository.Leer();
        if (dni.HasValue)
        {
            return inscripciones.Where(i => i.Dni == dni.Value).ToList();
        }

        return inscripciones;
    }

    public Inscripcion Registrar(Inscripcion nueva)
    {
        var talleres = _tallerRepository.Leer();
        var taller = talleres.FirstOrDefault(t => t.Id == nueva.TallerId);
        if (taller == null)
            throw new KeyNotFoundException($"No existe el taller con id {nueva.TallerId}.");

        var inscripciones = _inscripcionRepository.Leer();

        var activasTaller = inscripciones.Count(i => i.TallerId == taller.Id && i.EstaActiva());
        if (activasTaller >= taller.Cupo)
            throw new InvalidOperationException("El taller no tiene cupo disponible.");

        if (inscripciones.Any(i => i.Dni == nueva.Dni && i.TallerId == taller.Id && i.EstaActiva()))
            throw new InvalidOperationException("El alumno ya tiene una inscripción activa en este taller.");

        var activasAlumno = inscripciones.Count(i => i.Dni == nueva.Dni && i.EstaActiva());
        if (activasAlumno >= TOPE_TALLERES_UCSE)
            throw new InvalidOperationException($"El alumno no puede tener más de {TOPE_TALLERES_UCSE} inscripciones activas.");

        var talleresActivosAlumno = talleres
            .Where(t => inscripciones.Any(i => i.Dni == nueva.Dni && i.TallerId == t.Id && i.EstaActiva()))
            .ToList();

        if (talleresActivosAlumno.Any(t => t.SeSuperponeCon(taller)))
            throw new InvalidOperationException("El taller se superpone con otro taller en el que el alumno ya está inscripto.");

        nueva.FechaInscripcion = DateTime.Now;
        nueva.FechaBaja = null;

        nueva.Id = inscripciones.Count > 0 ? inscripciones.Max(i => i.Id) + 1 : 1;
        inscripciones.Add(nueva);
        _inscripcionRepository.Guardar(inscripciones);

        return nueva;
    }

    public Inscripcion RegistrarBaja(int id)
    {
        var inscripciones = _inscripcionRepository.Leer();
        var inscripcion = inscripciones.FirstOrDefault(i => i.Id == id);
        if (inscripcion == null)
            throw new KeyNotFoundException($"No existe la inscripción con id {id}.");

        if (!inscripcion.EstaActiva())
            throw new InvalidOperationException("La inscripción ya fue dada de baja.");

        inscripcion.FechaBaja = DateTime.Now;
        _inscripcionRepository.Guardar(inscripciones);

        return inscripcion;
    }
}
