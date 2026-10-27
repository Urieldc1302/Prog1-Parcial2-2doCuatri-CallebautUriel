using Talleres.Logica;

namespace Talleres.Api.DTOs;

public static class InscripcionMapper
{
    public static InscripcionResponse AInscripcionResponse(this Inscripcion inscripcion)
    {
        return new InscripcionResponse
        {
            Id = inscripcion.Id,
            TallerId = inscripcion.TallerId,
            Dni = inscripcion.Dni,
            NombreAlumno = inscripcion.NombreAlumno,
            FechaInscripcion = inscripcion.FechaInscripcion,
            FechaBaja = inscripcion.FechaBaja
        };
    }

    public static Inscripcion AInscripcion(this InscripcionRequest request)
    {
        return new Inscripcion
        {
            TallerId = request.TallerId!.Value,
            Dni = request.Dni!.Value,
            NombreAlumno = request.NombreAlumno.Trim()
        };
    }
}
