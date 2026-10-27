using Newtonsoft.Json;

namespace Talleres.Logica.Data;

public class TallerRepository
{
    private readonly string _rutaArchivo;

    public TallerRepository(string rutaArchivo)
    {
        _rutaArchivo = rutaArchivo;
    }

    public List<Taller> Leer()
    {
        CrearArchivoSiNoExiste();

        string json = File.ReadAllText(_rutaArchivo);
        if (string.IsNullOrWhiteSpace(json))
            return new List<Taller>();

        return JsonConvert.DeserializeObject<List<Taller>>(json) ?? new List<Taller>();
    }

    public void CrearArchivoSiNoExiste()
    {
        if (File.Exists(_rutaArchivo))
            return;

        string json = @"[
  { ""Id"": 1, ""Nombre"": ""Robótica"", ""Dia"": ""Lunes"", ""HoraInicio"": 18, ""HoraFin"": 20, ""Cupo"": 3 },
  { ""Id"": 2, ""Nombre"": ""Fotografía"", ""Dia"": ""Lunes"", ""HoraInicio"": 20, ""HoraFin"": 22, ""Cupo"": 2 },
  { ""Id"": 3, ""Nombre"": ""Ajedrez"", ""Dia"": ""Lunes"", ""HoraInicio"": 19, ""HoraFin"": 21, ""Cupo"": 4 },
  { ""Id"": 4, ""Nombre"": ""Teatro"", ""Dia"": ""Miércoles"", ""HoraInicio"": 18, ""HoraFin"": 20, ""Cupo"": 2 }
]";
        File.WriteAllText(_rutaArchivo, json);
    }
}
