using Microsoft.AspNetCore.Mvc;
using Talleres.Logica;

namespace Talleres.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TalleresController : ControllerBase
{
    private readonly InscripcionService _servicio;

    public TalleresController(InscripcionService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public IActionResult ObtenerTodos()
    {
        return Ok(_servicio.ObtenerTalleres());
    }
}
