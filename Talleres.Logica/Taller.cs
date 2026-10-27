namespace Talleres.Logica;

public class Taller
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Dia { get; set; } = string.Empty;
    public int HoraInicio { get; set; }
    public int HoraFin { get; set; }
    public int Cupo { get; set; }
    public int CuposDisponibles { get; set; }

    public bool SeSuperponeCon(Taller otro)
    {
        return Dia.Equals(otro.Dia, StringComparison.OrdinalIgnoreCase)
            && HoraInicio < otro.HoraFin
            && otro.HoraInicio < HoraFin;
    }
}
