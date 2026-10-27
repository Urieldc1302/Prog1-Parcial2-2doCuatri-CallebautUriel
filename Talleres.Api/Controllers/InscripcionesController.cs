using Microsoft.AspNetCore.Mvc;
using Talleres.Api.DTOs;
using Talleres.Logica;

namespace Talleres.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InscripcionesController : ControllerBase
{
    private readonly InscripcionService _servicio;

    public InscripcionesController(InscripcionService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public IActionResult ObtenerTodas([FromQuery] int? dni)
    {
        var inscripciones = _servicio.ObtenerInscripciones(dni);
        return Ok(inscripciones.Select(i => i.AInscripcionResponse()));
    }

    [HttpPost]
    public IActionResult Registrar([FromBody] InscripcionRequest request)
    {
        try
        {
            var inscripcion = _servicio.Registrar(request.AInscripcion());
            return StatusCode(201, inscripcion.AInscripcionResponse());
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Ocurrió un error inesperado", detalle = ex.Message });
        }
    }

    [HttpPut("{id}/baja")]
    public IActionResult RegistrarBaja(int id)
    {
        try
        {
            var inscripcion = _servicio.RegistrarBaja(id);
            return Ok(inscripcion.AInscripcionResponse());
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Ocurrió un error inesperado", detalle = ex.Message });
        }
    }
}
