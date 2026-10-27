using Newtonsoft.Json;

namespace Talleres.Logica.Data;

public class InscripcionRepository
{
    private readonly string _rutaArchivo;

    public InscripcionRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
    }

    public List<Inscripcion> Leer()
    {
        if (!File.Exists(_rutaArchivo))
            return new List<Inscripcion>();

        string json = File.ReadAllText(_rutaArchivo);
        if (string.IsNullOrWhiteSpace(json))
            return new List<Inscripcion>();

        return JsonConvert.DeserializeObject<List<Inscripcion>>(json) ?? new List<Inscripcion>();
    }

    public void Guardar(List<Inscripcion> inscripciones)
    {
        string json = JsonConvert.SerializeObject(inscripciones, Formatting.Indented);
        File.WriteAllText(_rutaArchivo, json);
    }
}
