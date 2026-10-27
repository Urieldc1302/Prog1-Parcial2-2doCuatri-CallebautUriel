using NUnit.Framework;
using Talleres.Logica;
using Talleres.Logica.Data;

namespace Talleres.Tests;

[TestFixture]
public class InscripcionServiceTests
{
    private string _rutaTalleres = string.Empty;
    private string _rutaInscripciones = string.Empty;

    [SetUp]
    public void SetUp()
    {
        _rutaTalleres = $"talleres_test_{Guid.NewGuid():N}.json";
        _rutaInscripciones = $"inscripciones_test_{Guid.NewGuid():N}.json";
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(_rutaTalleres)) File.Delete(_rutaTalleres);
        if (File.Exists(_rutaInscripciones)) File.Delete(_rutaInscripciones);
    }

    [Test]
    public void Registrar_TallerSinCupo_LanzaInvalidOperationException()
    {
        // Arrange
        var tallerRepo = new TallerRepository(_rutaTalleres);
        var inscripcionRepo = new InscripcionRepository(_rutaInscripciones);
        var servicio = new InscripcionService(tallerRepo, inscripcionRepo);

        servicio.Registrar(new Inscripcion { TallerId = 2, Dni = 40000001, NombreAlumno = "Alumno 1" });
        servicio.Registrar(new Inscripcion { TallerId = 2, Dni = 40000002, NombreAlumno = "Alumno 2" });

        var inscripcionExcedente = new Inscripcion { TallerId = 2, Dni = 40000003, NombreAlumno = "Alumno 3" };

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => servicio.Registrar(inscripcionExcedente));
    }

    [Test]
    public void Registrar_TallerQueIniciaJustoAlTerminarOtro_RegistraExitosamente()
    {
        // Arrange
        var tallerRepo = new TallerRepository(_rutaTalleres);
        var inscripcionRepo = new InscripcionRepository(_rutaInscripciones);
        var servicio = new InscripcionService(tallerRepo, inscripcionRepo);

        servicio.Registrar(new Inscripcion { TallerId = 1, Dni = 47075994, NombreAlumno = "Callebaut Uriel" });

        var segundaInscripcion = new Inscripcion { TallerId = 2, Dni = 47075994, NombreAlumno = "Callebaut Uriel" };

        // Act
        var resultado = servicio.Registrar(segundaInscripcion);

        // Assert
        Assert.That(resultado.Id, Is.GreaterThan(0));
        Assert.That(resultado.EstaActiva(), Is.True);
    }

    [Test]
    public void Registrar_TalleresSuperpuestos_LanzaInvalidOperationException()
    {
        // Arrange
        var tallerRepo = new TallerRepository(_rutaTalleres);
        var inscripcionRepo = new InscripcionRepository(_rutaInscripciones);
        var servicio = new InscripcionService(tallerRepo, inscripcionRepo);

        // Taller 1: Lunes 18 a 20. Taller 3: Lunes 19 a 21. Se superponen.
        servicio.Registrar(new Inscripcion { TallerId = 1, Dni = 47075994, NombreAlumno = "Callebaut Uriel" });

        var inscripcionSuperpuesta = new Inscripcion { TallerId = 3, Dni = 47075994, NombreAlumno = "Callebaut Uriel" };

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => servicio.Registrar(inscripcionSuperpuesta));
    }

    [Test]
    public void Registrar_SuperaTopeDeInscripcionesActivas_LanzaInvalidOperationException()
    {
        // Arrange
        var tallerRepo = new TallerRepository(_rutaTalleres);
        var inscripcionRepo = new InscripcionRepository(_rutaInscripciones);
        var servicio = new InscripcionService(tallerRepo, inscripcionRepo);

        // Taller 1: Lunes 18 a 20. Taller 4: Miércoles 18 a 20. (2 inscripciones activas)
        servicio.Registrar(new Inscripcion { TallerId = 1, Dni = 47075994, NombreAlumno = "Callebaut Uriel" });
        servicio.Registrar(new Inscripcion { TallerId = 4, Dni = 47075994, NombreAlumno = "Callebaut Uriel" });

        // Intento de tercera inscripción
        var terceraInscripcion = new Inscripcion { TallerId = 2, Dni = 47075994, NombreAlumno = "Callebaut Uriel" };

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => servicio.Registrar(terceraInscripcion));
    }
}
