using System.ComponentModel.DataAnnotations;

namespace Talleres.Api.DTOs;

public class InscripcionRequest
{
    [Required(ErrorMessage = "El id del taller es requerido.")]
    [Range(1, int.MaxValue, ErrorMessage = "El id del taller debe ser positivo.")]
    public int? TallerId { get; set; }

    [Required(ErrorMessage = "El DNI es requerido.")]
    [Range(1, int.MaxValue, ErrorMessage = "El DNI debe ser positivo.")]
    public int? Dni { get; set; }

    [Required(ErrorMessage = "El nombre del alumno es requerido.")]
    [MinLength(1, ErrorMessage = "El nombre del alumno no puede estar vacío.")]
    public string NombreAlumno { get; set; } = string.Empty;
}
